using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
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
}
