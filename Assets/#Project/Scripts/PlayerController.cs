using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    InputAction run;
    private NavMeshAgent agent;

    void OnEnable(){
        inputActions.FindActionMap("PlayerMap").Enable();
        run = inputActions["Run"];
        run.performed += OnSpace;
        run.canceled += OnSpaceRelease;
    }

    void OnDisable(){
        inputActions.FindActionMap("PlayerMap").Disable();
        run.performed -= OnSpace;
        run.canceled -= OnSpaceRelease;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        // //arrêt de la course
        // Keyboard keyboard = Keyboard.current;
        // if (keyboard.shiftKey.wasReleasedThisFrame){
        //     agent.speed/=5 ;
        //     Debug.Log(agent.speed);
        // }
        
        //raycast destination selon souris
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//donne un rayon qui part de la caméra, traverse l'écran et continue dans le monde
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction * 2000f, Color.magenta, 1f);
        if(Physics.Raycast(ray, out hit)){
            agent.SetDestination(hit.point);
        } //permet de faire rejoindre la souris, sans forcément cliquer
    }

    void OnSpace(InputAction.CallbackContext context){
        agent.speed *= 5;
    }

    void OnSpaceRelease(InputAction.CallbackContext context){
        agent.speed /= 5;
    }
}

