using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEditor.UI;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private int _hp = 10;
    
    public int hp
    {
        get {return _hp;}
        set{
            _hp = value;
        }
        
    }

    void Start(){
        if(gameObject.tag == "Enemy"){
            _hp = 10;
        }
        else if(gameObject.tag == "Interactible"){
            _hp = 5;
        }
    }
}
