using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))] 

public class RaycastLineRenderer : MonoBehaviour
{
    internal PlayerController playerController;
    private LineRenderer line;
    [SerializeField] private float range;
    
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, transform.position);
        // if(Physics.Raycast(transform.position, playerController.moveDirection, out RaycastHit hit, range)){
        //     line.SetPosition(1, hit.point); //propriété de raycastHit. Hit.point, c'est le point d'impact du rayon avec le collider
        //     line.startColor = Color.red;
        //     line.endColor = Color.red;
        // }
        // else{
        //     line.SetPosition(1, transform.position + transform.forward * range);
        //     line.startColor = Color.green;
        //     line.endColor = Color.green;
        // }
    }
}
