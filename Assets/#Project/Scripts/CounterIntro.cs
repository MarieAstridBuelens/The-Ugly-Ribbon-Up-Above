using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterIntro : MonoBehaviour
{
    
    private int travellingCounter = 0;
    private int state = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        travellingCounter++;
        switch(state){
            case 0:
                transform.position += new Vector3(-0.02f, -0.04f, -0.005f);
                if(travellingCounter > 8700){
                    state = 1;
                }
                break;
            case 1:
                transform.position += new Vector3(-0.07f, 0.04f, -0.01f);
                if(travellingCounter > 12000){
                    state = 2;
                }
                break;
            case 2:
                transform.position += new Vector3(0.01f, 0.03f, -0.01f);
                // if(travellingCounter > 12000){
                //     state = 2;
                // }
                break;
        }
    }
}
