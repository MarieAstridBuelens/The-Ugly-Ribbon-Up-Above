using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            SceneManager.LoadScene(sceneName); 
        } 
    }

    // float time = 5f;
    // [SerializeField] float delay = 5f; //variable delay
    // [SerializeField] private string sceneName;
    // [SerializeField] private UnityEvent whenLevelFinished;
    // private bool counterOn = false;

    // void Start(){
    //     time = delay;
    // }
    

    // void OnTriggerEnter(Collider other){
    //     if(other.tag == "Player"){
    //         counterOn = true;
    //         //Debug.Log("triggered");
    //     } 
    // }

    // void Update(){ //sert à mettre un délai avant le chargement de la scène, uniquement après que la fonction Change ait été appelée.
    //     if (counterOn){
    //         if(time < delay){
    //             time += Time.deltaTime;

    //             if(time >= delay){
    //                 sceneName = sceneName.Trim();
    //                 time = 0f;
    //                 counterOn = false;
    //                 SceneManager.LoadScene(sceneName); 
    //                 this.sceneName = sceneName;
    //             }
    //         }
    //     }
    // }
}
