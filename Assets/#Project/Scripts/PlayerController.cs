using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]


public class PlayerController : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float throwSpeed = 0.3f;

    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform shoulder;

    //InputAction run;
    InputAction move;
    InputAction sideSteps;
    InputAction basicStrikeControl;
    InputAction deathRayControl;
    InputAction telekinesis;
    private NavMeshAgent agent;
    Keyboard keyboard = Keyboard.current;
    Mouse mouse = Mouse.current;
    [SerializeField] private float rayLength = 60f;
    internal Collider newCollider = null;
    private Color col = Color.blue;
    internal Transform destructible;
    [SerializeField] internal Transform resetDestructible;
    private bool _canDeckOnShoulder = true;
    public bool canDeckOnShoulder{
        get{return _canDeckOnShoulder;}
        set{_canDeckOnShoulder = value;}
    }
    [SerializeField] private LineRenderer throwLine;
    [SerializeField] private Color throwCol = Color.blue;
    internal bool movingToTarget = false;

    internal bool canDeathRay = true;

    [SerializeField] private GameObject prefab;
    private float spawnLeftOversChrono = 10f;
    private bool goLeftOversChrono = false;
    [SerializeField] private Transform savedTransform;

    [SerializeField] private float deathRayLength = 60f;
    [SerializeField] private float telekinesisLength = 60f;
    [SerializeField] private float throwLength = 60f;

    internal Renderer[] rendererBodyParts;
    [SerializeField] internal Collider savedCollider;
    //[SerializeField] private Light sceneLight;

    //booleans for sound
    internal bool nEvilMoving = false;
    internal AudioManager audiomanager;
    internal bool askAudioManagerBasicStrikeSound = false;
    internal bool askAudioManagerBowHighLight = false;
    internal float bowHighlighCounter = 0f;
    internal bool bowIsDestroyed = false;
    internal float basicStrikePlayerRotationCounter = 50f;
    internal bool basicStrikeRotation = false;


    void OnEnable()
    {
        inputActions.FindActionMap("PlayerMap").Enable();
        // run = inputActions["Run"];
        // run.performed += OnSpace;
        // run.canceled += OnSpaceRelease;

        move = inputActions["Move"];
        sideSteps = inputActions["SideSteps"];
        basicStrikeControl = inputActions["BasicStrikeControl"];
        deathRayControl = inputActions["DeathRayControl"];
        telekinesis = inputActions["Telekinesis"];
    }

    void OnDisable()
    {
        inputActions.FindActionMap("PlayerMap").Disable();
        // run.performed -= OnSpace;
        // run.canceled -= OnSpaceRelease;
    }


    void Start()
    {
        //Debug.Log("Start");
        //assign a callback for BasicStrike and DeathRay
        basicStrikeControl.performed += BasicStrike;
        deathRayControl.performed += DeathRay;
        telekinesis.performed += TelekinesisActived;
        telekinesis.canceled += TelekinesisThrow;

        throwLine.enabled = false;

        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        MoveControls();
        EnlightInteractible();

        if (!canDeckOnShoulder)
        {
            CheckThrowLine();
        }

        if (movingToTarget)
        {
            ThrowingToTarget();
        }

        if(goLeftOversChrono){
            SpawnLeftOvers();
        }
        if(basicStrikeRotation){
            basicStrikePlayerRotationCounter --;
            if(basicStrikePlayerRotationCounter<=0){
                basicStrikeRotation = false;
                basicStrikePlayerRotationCounter = 50f;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }

        // //arrêt de la course
        // Keyboard keyboard = Keyboard.current;
        // if (keyboard.shiftKey.wasReleasedThisFrame){
        //     agent.speed/=5 ;
        //     Debug.Log(agent.speed);
        // }

        //raycast destination selon souris
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//donne un rayon qui part de la caméra, traverse l'écran et continue dans le monde
        // RaycastHit hit;
        // //Debug.DrawRay(ray.origin, ray.direction * 2000f, Color.magenta, 1f);
        // if(Physics.Raycast(ray, out hit)){
        //     agent.SetDestination(hit.point);
        //} //permet de faire rejoindre la souris, sans forcément cliquer
    }


    void MoveControls()
    {

        Vector3 moveAmount = move.ReadValue<Vector3>();
        //Debug.Log(moveAmount);
        Vector3 sideAmount = sideSteps.ReadValue<Vector3>();

        if (moveAmount == Vector3.zero && sideAmount == Vector3.zero) {
            nEvilMoving = false;
            return;
        } // SINONla direction indiquée va quand même donner un vectuer qui initie un mouvement !
        else  nEvilMoving = true;

        //rotation du Player vers sa direction
        //Atan2 = fonction qui retourne l'angle en radiants entre axe x et le vecteur allant de (0,0) à (moveAmount.x, moveAmount.y)
        //Mathf.Rad2Deg : traduit radiants en degrés
        //Quaternion.Euler nous permet de donner trois nombre, la rotation autour de x, y et z.
        float targetAngle = Mathf.Atan2(moveAmount.x, moveAmount.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        //assouplir la rotation du Player
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //donne la direction vers laquelle on veut aller en prenant en compte l'angle de la caméra
        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        transform.position += new Vector3(moveDirection.x, moveDirection.y, moveDirection.z).normalized * Time.deltaTime * speed;

        //direction des sidesteps
        transform.position += new Vector3(sideAmount.x, sideAmount.y, sideAmount.z).normalized * Time.deltaTime * speed;
    }

    // void OnSpace(InputAction.CallbackContext context){
    //     agent.speed *= 5;
    // }

    // void OnSpaceRelease(InputAction.CallbackContext context){
    //     agent.speed /= 5;
    // }



    public void BasicStrike(InputAction.CallbackContext context)
    {

        //Debug.Log("clicked!");
        askAudioManagerBasicStrikeSound = true;
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), transform.forward * 50, Color.magenta, 1f);
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out RaycastHit hit, rayLength))
        {
            //Debug.Log("I shot");
            //Debug.DrawRay(transform.position, transform.forward * rayLength, col);
            HealthManager enemyHp = hit.collider.GetComponent<HealthManager>();
            if (enemyHp != null)
            {
                enemyHp.hp -= 1;
                Debug.Log(enemyHp.hp);
                basicStrikeRotation = true;
                transform.rotation = Quaternion.Euler(0f, 45f, 0);
                if (enemyHp.hp <= 0)
                {
                    if(enemyHp.tag == "Interactible"){
                        bowIsDestroyed = true;
                    }
                    savedTransform = enemyHp.transform;
                    enemyHp.gameObject.SetActive(false);
                    goLeftOversChrono = true;
                    //Destroy(hit.transform.gameObject);
                    //Debug.Log("Leftovers appear");
                }

            }
            
        }
    }

    public void SpawnLeftOvers(){
        Debug.Log("Leftovers spawned");

        Instantiate(prefab, savedTransform.position, savedTransform.rotation);
        spawnLeftOversChrono--;
        if(spawnLeftOversChrono <= 0){
            goLeftOversChrono = false;
            spawnLeftOversChrono = 10f;
        }
    }

    public void EnlightInteractible()
    {
        float defaultIntensity = 0.42f;
        
        Debug.DrawRay(transform.position, transform.forward *  deathRayLength, col);
        
        newCollider = null;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, deathRayLength))
        {
            Collider coll = hit.collider.GetComponent<Collider>();
            if (coll.TryGetComponent(out Renderer renderer))
            {
                newCollider = hit.collider;
                if(newCollider.tag == "Enemy"){
                    renderer.material.SetFloat("_Glow", 0.661f);
                    rendererBodyParts = coll.GetComponentsInChildren<Renderer>();

                    foreach(Renderer bodyPart in rendererBodyParts)
                        bodyPart.material.SetFloat("_Glow", 0.661f);
                    Debug.Log("shiny head");
                }
                else if(newCollider.tag == "Interactible"){
                    //Debug.Log(RenderSettings.ambientIntensity);
                    askAudioManagerBowHighLight = true;
                    bowHighlighCounter --;
                    renderer.material.SetFloat("_Glow", 0.661f);
                    //quand bow hightlighted, change la lumière de la scène de façon enchanteuse
                    RenderSettings.ambientIntensity = Mathf.PingPong(Time.time * 2.35f, 5);
                    Debug.Log(RenderSettings.ambientIntensity);
                    //rdrSettings.ambientIntensity = Mathf.PingPong(Time.time, 8); // pingpong change graduellement la lumière jusqu'à l'intensité demandé, 8 étant le maximum
                }
                
            }

            else
            {
                Debug.LogWarning($"There is no renderer on {coll.name}");
            }

            if(savedCollider != null && (newCollider == null || newCollider != savedCollider)){
                if (savedCollider.TryGetComponent(out Renderer rend))
                {
                rend.material.SetFloat("_Glow", 0.0f);
                askAudioManagerBowHighLight = false;
                rendererBodyParts = savedCollider.GetComponentsInChildren<Renderer>();
                RenderSettings.ambientIntensity = defaultIntensity;
                //Debug.Log("REVERSE" + RenderSettings.ambientIntensity);
                foreach(Renderer bodyPart in rendererBodyParts)
                    bodyPart.material.SetFloat("_Glow", 0.0f);
                Debug.Log("not shiny head");
                }
            }

            savedCollider = newCollider;
        }
    }

    public void DeathRay(InputAction.CallbackContext context)
    {

        Debug.Log("DeathRay activated!");
        Debug.Log($"can ray? {canDeathRay}");
        Debug.DrawRay(transform.position, transform.forward * 10, Color.magenta, 10f);
        if (canDeathRay && Physics.Raycast(transform.position, transform.forward, out RaycastHit hit3, deathRayLength))
        {
            Debug.Log($"Ray Touch: {hit3.collider.name}");
            Debug.DrawRay(transform.position, transform.forward * deathRayLength, col);
            if (hit3.collider.tag == "Enemy")
            {
                Debug.Log("ARGH! A DEATHRAY ATTACK!");
                savedTransform = hit3.collider.transform;
                hit3.collider.gameObject.SetActive(false);
                goLeftOversChrono = true;
                //Destroy(hit.transform.gameObject);
                Debug.Log("Leftovers appear");
                //Destroy(hit3.transform.gameObject);
            }
        }

    }

    public void TelekinesisActived(InputAction.CallbackContext context)
    {
        Debug.Log("You're now in the Telekinesis mode!");
        if (canDeckOnShoulder && Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, telekinesisLength))
        {
            Debug.DrawRay(transform.position, transform.forward * telekinesisLength, col);

            if (hit.transform.GetComponent<NavMeshAgent>())
            {
                NavMeshAgent agent = hit.transform.GetComponent<NavMeshAgent>();
                agent.enabled = false;
            }

            destructible = hit.transform.GetComponent<Transform>();
            destructible.SetParent(shoulder);
            destructible.transform.localPosition = new Vector3(0, 0, 0);
            canDeckOnShoulder = false;
            canDeathRay = false;



            // Vector3 pos =  destructible.position;
            // pos.y = transform.position.y + 4;
            // pos.x = transform.position.x + 2;
            // pos.z = transform.position.z;
            // destructible.position = pos;

            //penser à une rotation de 90°

        }

    }

    public void CheckThrowLine()
    {
        if (canDeckOnShoulder && Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, throwLength))
        {
            Debug.DrawRay(transform.position, transform.forward * throwLength, col);
            throwLine.enabled = true;
            throwLine.startColor = throwCol;
            throwLine.endColor = throwCol;
            throwLine.SetPosition(0, shoulder.position);
            throwLine.SetPosition(1, hit.point);
        }
        else
        {
            throwLine.enabled = false;
        }
    }

    public void TelekinesisThrow(InputAction.CallbackContext context)
    {
        Debug.Log("Take that!");
        destructible.SetParent(null);
        movingToTarget = true;

    }

    public void ThrowingToTarget()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, throwLength))
        {
            Vector3 placeToHit = hit.point + new Vector3(0, 2, 0);
            destructible.transform.position = Vector3.MoveTowards(destructible.transform.position, placeToHit, throwSpeed);
        }
        throwLine.enabled = false;

    }



}



