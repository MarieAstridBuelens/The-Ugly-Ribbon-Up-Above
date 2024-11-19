using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    
    float time = 5f;
    [SerializeField] float delay = 5f; //variable delay
    private string sceneName;

    void Start(){
        time = delay;
    }
    
    void Update(){ //sert à mettre un délai avant le chargement de la scène, uniquement après que la fonction Change ait été appelée.
        if(time < delay){
            time += Time.deltaTime;
            if(time >= delay){
                SceneManager.LoadScene(sceneName);
            }
        }
    }
    
    public void Change(string sceneName){
        sceneName = sceneName.Trim();
        this.sceneName = sceneName;
        time = 0f;
    }

}
