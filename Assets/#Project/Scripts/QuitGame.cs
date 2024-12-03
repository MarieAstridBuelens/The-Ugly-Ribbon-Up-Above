using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void ExitGame() {
        Application.Quit();
        Debug.Log("Game is exiting");
        //Just to make sure it's working
    }
}
