using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutoTrigger : MonoBehaviour
{
    [SerializeField]internal GraphicRaycaster graphicRaycaster;
    internal TextMeshProUGUI[] tmp;
    internal TextMeshProUGUI tmp_dialogueBox;
    internal TextMeshProUGUI tmp_tutoBox;
    
    [SerializeField] internal string txt;

    private int counter = 1600;
    private bool counterOn = false;
    
    void Start()
    {
        graphicRaycaster = FindObjectOfType<GraphicRaycaster>(includeInactive: true);
        tmp = graphicRaycaster.GetComponentsInChildren<TextMeshProUGUI>(includeInactive: true);
        counterOn = true;
        tmp_dialogueBox = tmp[0];
        tmp_tutoBox = tmp[1];
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            Debug.Log("triggered");
            tmp_tutoBox.gameObject.SetActive(true);
            tmp_tutoBox.text = txt;
            counterOn = true;
        }
    }

     void Update(){
        if(counterOn){
            counter--;
            if (counter <= 0){
                tmp_tutoBox.gameObject.SetActive(false);
                counterOn = false;
                counter = 1600;
            }

        }
    }
}
