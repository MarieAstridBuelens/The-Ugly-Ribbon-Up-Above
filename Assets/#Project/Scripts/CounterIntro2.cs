using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CounterIntro2 : MonoBehaviour
{
    
    internal float travellingCounter = 0f;
    private int state = 0;
    private float accel = 1.5f;
    [SerializeField] internal TextMeshProUGUI tmp_narrativeBox;
    [SerializeField] Canvas imageCanvas;
    internal Image[] introImages;
    [SerializeField] string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        introImages = imageCanvas.GetComponentsInChildren<Image>(includeInactive: true);
        Debug.Log(introImages.Length);
    }

    // Update is called once per frame
    void Update()
    {
        travellingCounter += Time.deltaTime;
        accel -= 0.0001f;

        
        switch(state){
            case 0:
                transform.position += new Vector3(-0.02f, -0.04f, -0.005f) * accel * Time.deltaTime * 520;
                if(travellingCounter > 5f){
                    tmp_narrativeBox.text = "After all I sacrified";
                }

                if(travellingCounter > 7f){
                    tmp_narrativeBox.text = "I reached the end of my journey.";
                }

                if(travellingCounter > 10f){
                    tmp_narrativeBox.text = "So many were on my path";
                }

                if(travellingCounter > 13f){
                    state = 1;
                    accel = 1.5f;
                }
                break;
            case 1:
                transform.position += new Vector3(-0.07f, 0.04f, -0.01f) * accel * Time.deltaTime * 600;
                tmp_narrativeBox.text = "I nearly lost my soul at some point";

                if(travellingCounter > 16.5f){
                    tmp_narrativeBox.text = "But it finally paid off";
                }

                if(travellingCounter > 19f){
                    state = 2;
                    accel = 1.5f;
                }
                break;
            case 2:
                transform.position += new Vector3(0.01f, 0.04f, -0.01f) * accel * Time.deltaTime * 600;
                if(travellingCounter > 20f){
                    tmp_narrativeBox.text = "Now I'm heading home";
                }
                if(travellingCounter > 22.5f){
                    tmp_narrativeBox.text = "To my beautiful kingdom of evil and villainy";
                }
                if(travellingCounter > 26.5f){
                    tmp_narrativeBox.text = "And you know what?";
                }
                if(travellingCounter > 28.5f){
                    state = 3;
                    accel = 1.5f;
                }
                break;
            case 3:
                introImages[2].gameObject.SetActive(true);
                tmp_narrativeBox.text = "I love this place.";
                transform.position += new Vector3(0, 0, -0.02f) * accel * Time.deltaTime * 600;
                if(travellingCounter > 33f){
                    state = 4;
                    accel = 1.5f;
                }
                break;
            case 4:
                Change();
                break;
        }
    }

    public void Change(){
        SceneManager.LoadScene(sceneName);
    }
}
