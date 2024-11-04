using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetWithJuiciness : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 decal = new(0, 4, -8);
    

    // Update is called once per frame
    void Update()
    {
        //RotateWithTarget();
        Follow();
    }

    private void Follow(){
        transform.position = target.position + decal;
        // if (Vector3.Distance(transform.position, position) <= threshold){//donne distance entre ces deux positions, c'est une distance absolue
        //     transform.position = position;
        // }
        // else{
        //     Vector3 direction = (position - transform.position).normalized;
        //     transform.Translate(speed * Time.deltaTime * direction);
        // }
        //mouvement plus élastique ; soit près et se colle à la vitesse, soit suit à sa vitesse.
        // peut prendre du retard si accélération
    
    }

    private void RotateWithTarget(){
        //transform.rotation = target.rotation;
    }
}
