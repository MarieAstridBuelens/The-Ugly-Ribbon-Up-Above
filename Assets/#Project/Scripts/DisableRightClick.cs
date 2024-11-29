using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRightClick : MonoBehaviour
{
    internal PlayerController playerController;

    void Start(){
        playerController = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter(Collider other){
        Debug.Log("right-click disabled");
        if(other.tag == "Player"){
            Debug.Log(playerController.canDeckOnShoulder);
            playerController.canDeckOnShoulder = false;
            Debug.Log(playerController.canDeckOnShoulder);
        }
    }
}
