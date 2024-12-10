using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    internal PlayerController playerController;
    internal Slider healthBar;
    internal HealthManager enemyHP;
    internal int savedHp = 10;

    
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
            healthBar.gameObject.SetActive(true);
            enemyHP = playerController.newCollider.GetComponent<HealthManager>();
            healthBar.maxValue = enemyHP.maxHP;
            healthBar.value = enemyHP.hp;
            savedHp = enemyHP.hp;
        }
        if(savedHp <=0){
            healthBar.gameObject.SetActive(false);
            savedHp = 10;
        }


        // if(playerController.savedCollider != null && (playerController.newCollider == null || playerController.newCollider != playerController.savedCollider)){
        //     healthBar.gameObject.SetActive(false);
        // }
    }
}
