using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewGame : MonoBehaviour
{
    
    public void Change(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}

