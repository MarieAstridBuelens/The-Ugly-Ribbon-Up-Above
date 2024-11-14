using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WhenCollision : MonoBehaviour
{
    [SerializeField]internal PlayerController playerController;

    void Start(){
        playerController = FindObjectOfType<PlayerController>();
    }

    public void OnTriggerEnter(Collider other){
        playerController.movingToTarget = false;

        if(playerController.destructible.transform.GetComponent<UnityEngine.AI.NavMeshAgent>()){
            UnityEngine.AI.NavMeshAgent agent = playerController.destructible.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.enabled = true;
        }
        playerController.canDeathRay = true;
        playerController.canDeckOnShoulder = true;
        playerController.destructible = playerController.resetDestructible;
    }
}
