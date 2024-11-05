using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicStrike : MonoBehaviour
{
    
    [SerializeField] private float rayLength = 10f;
    private Color col = Color.blue;
    

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayLength)){
            Debug.DrawRay(transform.position, transform.forward * rayLength, col);
            if (hit.collider.tag == "Enemy"){
                Debug.Log("ARGH!");
                Destroy(hit.transform.gameObject);
            }
        }
        
        
    }
}
