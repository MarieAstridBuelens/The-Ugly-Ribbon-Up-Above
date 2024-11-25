using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    [SerializeField] AudioSource nEvilSound;
    [SerializeField] AudioSource bowSound;
    [SerializeField] AudioSource daveSound;
    internal PlayerController playerController;
    internal AudioSource playerAudioSource;
    private int startingCounter = 1600;
    

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerAudioSource = playerController.GetComponent<AudioSource>();
        playerAudioSource.clip = playerController.GetComponent<NEvilSound>().startLevel;

    }

    // Update is called once per frame
    void Update()
    {
        if(startingCounter > 0) startingCounter--;
        
        if(!playerController.nEvilMoving && startingCounter <= 0){
            
            playerAudioSource.clip = playerController.GetComponent<NEvilSound>().footsteps;
            playerAudioSource.loop = true;
            playerAudioSource.Play();
            Debug.Log(playerAudioSource.clip);
        }
    }
}
