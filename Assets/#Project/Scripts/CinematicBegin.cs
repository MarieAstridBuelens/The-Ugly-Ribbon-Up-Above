using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicBegin : MonoBehaviour
{
    internal PlayerController playerController;
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private AudioSource violetAudioSource;
    [SerializeField] private AudioSource globalAudioSource;
    [SerializeField] private GameObject nevilHood;
    [SerializeField] private GameObject nevilHead;

    void Start(){
        playerController = FindObjectOfType<PlayerController>();
    }
   
    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            playerController.OnDisable();
            playableDirector.Play();
            violetAudioSource.volume = 0.2f;
            globalAudioSource.volume = 0.2f;
            nevilHood.transform.rotation = Quaternion.Euler(0, 90, -30);
            nevilHood.transform.position += new Vector3(0, 0, -0.1f);
            nevilHead.transform.position += new Vector3(0, 0, 0);
        }
            
    }
}
