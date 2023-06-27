using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushOutOfCollider : MonoBehaviour
{

    /*This script will be used to ensure the obejct it is attached to does not glitch inside/through colliders.
     */
    Collider collider;

    LayerMask collidersToPushOut;
    private void Awake()
    {
        collider = GetComponent<Collider>();    
    }

    private void OnTriggerStay(Collider collision)
    {
        if(collider.gameObject.layer == collidersToPushOut)
        Debug.Log("Hello");
        if (collider.bounds.Intersects(collision.GetComponent<Collider>().bounds)) //if object inside collider
        {
            Debug.Log("inside me");
            collision.transform.position = new Vector3(collision.transform.position.x, (collider.bounds.center.y + collider.bounds.size.y /2)- .1f, collision.transform.position.z);
        }
    }



}
