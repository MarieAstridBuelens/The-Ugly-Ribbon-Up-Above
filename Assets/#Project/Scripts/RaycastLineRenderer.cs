using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))] 

public class RaycastLineRenderer : MonoBehaviour
{
    internal PlayerController playerController;
    private LineRenderer line;
    [SerializeField] private int rayLength = 30;

    
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayLength)){
            if(hit.collider.tag == "Enemy"){
                line.enabled = true;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, hit.point); //propriété de raycastHit. Hit.point, c'est le point d'impact du rayon avec le collider
                line.startColor = Color.red;
                line.endColor = Color.red;
            }
            else{
                line.enabled = false;
            }
        }
        else{
            line.enabled = false;
        }
        
    }
}
