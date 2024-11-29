using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    internal PlayerController playerController;
    internal Slider healthBar;
    internal HealthManager enemyHP;

    
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        healthBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.newCollider != null){
            enemyHP = playerController.newCollider.GetComponent<HealthManager>();
            healthBar.maxValue = enemyHP.maxHP;
            healthBar.value = enemyHP.hp;
        }
    }
}
