using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]internal GraphicRaycaster graphicRaycaster;
    internal TextMeshProUGUI[] tmp;
    internal TextMeshProUGUI tmp_dialogueBox;
    internal TextMeshProUGUI tmp_tutoBox;
    
    [SerializeField] internal string txt;
    private int counter = 1000;
    private bool counterOn = false;
    private Collider gameObjectBoxCollider;
    [SerializeField] AudioSource voiceAudioSource;
    [SerializeField] AudioClip audioClipToGive;

    
    void Start()
    {
        graphicRaycaster = FindObjectOfType<GraphicRaycaster>(includeInactive: true);
        tmp = graphicRaycaster.GetComponentsInChildren<TextMeshProUGUI>(includeInactive: true);
        tmp_dialogueBox = tmp[0];
        tmp_tutoBox = tmp[1];
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            //Debug.Log("triggered");
            counterOn = true;
            tmp_dialogueBox.gameObject.SetActive(true);
            tmp_dialogueBox.text = txt;
            voiceAudioSource.clip = audioClipToGive;
            voiceAudioSource.Play();
        }
    }

    void Update(){
        if(counterOn){
            counter--;
            if (counter <= 0){
                tmp_dialogueBox.gameObject.SetActive(false);
                gameObjectBoxCollider = gameObject.GetComponent<Collider>();
                gameObjectBoxCollider.gameObject.SetActive(false);
                counterOn = false;
                counter = 1000;
            }

        }
    }
}
