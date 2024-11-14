using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
public class WhenCollision : MonoBehaviour
{
    [SerializeField]internal PlayerController playerController;
    [SerializeField]internal HealthManager healthManager;

    void Start(){
        playerController = FindObjectOfType<PlayerController>();
        healthManager = GetComponent<HealthManager>();
    }

    public void OnTriggerEnter(Collider other){
        if(!playerController.movingToTarget) return;
        playerController.movingToTarget = false;

        if (other.TryGetComponent(out HealthManager otherHp)){
            otherHp.hp -= 3;
            healthManager.hp -= 3;
            Debug.Log($"{name} --> {other.name} : {otherHp.hp}");
            if(otherHp.hp <= 0){
                Destroy(other.transform.gameObject);
            } 
        }
        

        if(playerController.destructible.transform.GetComponent<UnityEngine.AI.NavMeshAgent>()){
            UnityEngine.AI.NavMeshAgent agent = playerController.destructible.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.enabled = true;
        }
        playerController.canDeathRay = true;
        playerController.canDeckOnShoulder = true;
        playerController.destructible = playerController.resetDestructible;
    }
}
