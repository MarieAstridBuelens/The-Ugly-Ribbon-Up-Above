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
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity;
    [SerializeField] private Transform cam;

    //InputAction run;
    InputAction move;
    InputAction sideSteps;
    InputAction basicStrikeControl;
    private NavMeshAgent agent;
    Keyboard keyboard = Keyboard.current;
    Mouse mouse = Mouse.current;
    float leftClicked;
    [SerializeField] private float rayLength = 10f;
    private Color col = Color.blue;

    [SerializeField] private GameObject objectHit = null;

    void OnEnable(){
        inputActions.FindActionMap("PlayerMap").Enable();
        // run = inputActions["Run"];
        // run.performed += OnSpace;
        // run.canceled += OnSpaceRelease;

        move = inputActions["Move"];
        sideSteps = inputActions["SideSteps"];
        basicStrikeControl = inputActions["BasicStrikeControl"];
    }

    void OnDisable(){
        inputActions.FindActionMap("PlayerMap").Disable();
        // run.performed -= OnSpace;
        // run.canceled -= OnSpaceRelease;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        MoveControls();
        SideControls();
        BasicStrike();

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

    
    void MoveControls(){
        
        Vector3 moveAmount = move.ReadValue<Vector3>();
        //Debug.Log(moveAmount);

        if(moveAmount == Vector3.zero) return; // SINONla direction indiquée va quand même donner un vectuer qui initie un mouvement !

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
    }

    void SideControls(){
        Vector3 sideAmount = sideSteps.ReadValue<Vector3>();
        transform.position += new Vector3(sideAmount.x, sideAmount.y, sideAmount.z).normalized * Time.deltaTime * speed;
    }

    // void OnSpace(InputAction.CallbackContext context){
    //     agent.speed *= 5;
    // }

    // void OnSpaceRelease(InputAction.CallbackContext context){
    //     agent.speed /= 5;
    // }



    public void BasicStrike(){
        leftClicked = basicStrikeControl.ReadValue<float>();
        if(leftClicked > 0){
            Debug.Log("clicked!");
            if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayLength)){
                Debug.DrawRay(transform.position, transform.forward * rayLength, col);
                if (hit.collider.tag == "Enemy"){
                    Debug.Log("ARGH!");
                    Destroy(hit.transform.gameObject);
                }
            }
            leftClicked = 0;
        }
        
    }

    public void DeathRay(){
        Destroy(gameObject);
    }
    
}

