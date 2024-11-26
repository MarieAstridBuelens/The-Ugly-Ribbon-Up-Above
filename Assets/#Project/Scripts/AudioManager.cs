using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    [SerializeField] internal AudioSource nEvilSound;
    [SerializeField] internal AudioSource bowSound;
    [SerializeField] internal AudioSource daveSound;
    internal PlayerController playerController;
    internal AudioSource playerAudioSource;
    internal int startingCounter = 620;
    internal AudioSource movingNEvilSound;
    internal AudioSource basicStrikeSound;
    internal AudioSource[] allAudioSources;
    

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        allAudioSources = playerController.GetComponentsInChildren<AudioSource>();
        playerAudioSource = allAudioSources[0];
        playerAudioSource.clip = playerController.GetComponent<NEvilSound>().startLevel;

        basicStrikeSound = allAudioSources[1];
        basicStrikeSound.clip = playerController.GetComponent<NEvilSound>().basicStrike;
        

    }

    // Update is called once per frame
    void Update()
    {
        if(startingCounter > 0) startingCounter--;
        
        if(!playerController.nEvilMoving && startingCounter <= 0){
            
            playerAudioSource.clip = playerController.GetComponent<NEvilSound>().footsteps;
            playerAudioSource.loop = true;
            playerAudioSource.pitch = Random.Range(0.9f, 1.1f);
            playerAudioSource.Play();
            Debug.Log(playerAudioSource.clip);
        }

        if(playerController.askAudioManagerBasicStrikeSound){
            Debug.Log("hit sound playing");
            basicStrikeSound.PlayOneShot(basicStrikeSound.clip, 1f);
            playerController.askAudioManagerBasicStrikeSound = false;
        }

    //   if(playerController.nEvilBasicShooting){
    //         playerAudioSource.clip = playerController.GetComponent<NEvilSound>().basicStrike;
    //         playerAudioSource.loop = false;
    //         playerAudioSource.Play();
    //         Debug.Log("shooting noise");
    //     }
    }

    // public void BasicStrikeSound(){
    //     Debug.Log("hit sound playing");
    //     basicStrikeSound.PlayOneShot(basicStrikeSound.clip, 1f);
    // }
}
