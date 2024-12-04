using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CounterIntro : MonoBehaviour
{
    
    internal int travellingCounter = 0;
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
        travellingCounter++;
        accel -= 0.0001f;

        
        switch(state){
            case 0:
                transform.position += new Vector3(-0.02f, -0.04f, -0.005f) * accel;
                if(travellingCounter > 1600){
                    tmp_narrativeBox.text = "After all I sacrified";
                }

                if(travellingCounter > 3200){
                    tmp_narrativeBox.text = "I reached the end of my journey.";
                }

                if(travellingCounter > 5700){
                    tmp_narrativeBox.text = "So many were on my path";
                }

                if(travellingCounter > 8700){
                    state = 1;
                    accel = 1.5f;
                }
                break;
            case 1:
                transform.position += new Vector3(-0.07f, 0.04f, -0.01f) * accel;
                tmp_narrativeBox.text = "I nearly lost my soul at some point";

                if(travellingCounter > 11000){
                    tmp_narrativeBox.text = "But it finally paid off";
                }

                if(travellingCounter > 12000){
                    state = 2;
                    accel = 1.5f;
                }
                break;
            case 2:
                transform.position += new Vector3(0.01f, 0.04f, -0.01f) * accel;
                if(travellingCounter > 12500){
                    tmp_narrativeBox.text = "Now I'm heading home";
                }
                if(travellingCounter > 14700){
                    tmp_narrativeBox.text = "To my beautiful kingdom of evil and villainy";
                }
                if(travellingCounter > 17800){
                    tmp_narrativeBox.text = "And you know what?";
                }
                if(travellingCounter > 19500){
                    state = 3;
                    accel = 1.5f;
                }
                break;
            case 3:
                introImages[2].gameObject.SetActive(true);
                tmp_narrativeBox.text = "I love this place.";
                transform.position += new Vector3(0, 0, -0.02f) * accel;
                if(travellingCounter > 22500){
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
