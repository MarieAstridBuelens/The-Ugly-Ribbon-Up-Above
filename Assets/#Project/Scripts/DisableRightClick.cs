using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRightClick : MonoBehaviour
{
    internal PlayerController playerController;

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            playerController.canDeckOnShoulder = false;
        }
    }
}
