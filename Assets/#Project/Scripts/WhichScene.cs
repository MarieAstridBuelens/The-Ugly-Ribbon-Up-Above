using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class WhichScene : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] private string level;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void OnTriggerEnter()
    {
        playerController.currentLevel = level;
        Debug.Log(level);
    }
}
