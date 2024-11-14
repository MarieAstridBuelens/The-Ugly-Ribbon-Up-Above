using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RaycastLineRenderer : MonoBehaviour
{
    internal PlayerController playerController;
    [SerializeField] private LineRenderer deathRayLine;
    [SerializeField] private Color deathRayCol = Color.green;
    [SerializeField] private int rayLength = 30;

    
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        //deathRayLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.canDeathRay && Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayLength)){
            if(hit.collider.tag == "Enemy"){
                deathRayLine.enabled = true;
                deathRayLine.SetPosition(0, transform.position);
                deathRayLine.SetPosition(1, hit.point); //propriété de raycastHit. Hit.point, c'est le point d'impact du rayon avec le collider
                deathRayLine.startColor = deathRayCol;
                deathRayLine.endColor = deathRayCol;
            }
            else{
                deathRayLine.enabled = false;
            }
        }
        else{
            deathRayLine.enabled = false;
        }
        
    }
}
