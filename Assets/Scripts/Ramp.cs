using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    [SerializeField] float force, yStartPos;
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(collision.GetComponent<Movement>().airborneFromRamp)
            {
                return;
            }
            var rb = collision.GetComponent<Rigidbody>();
            rb.transform.position = new Vector3(rb.transform.position.x, yStartPos, rb.transform.position.z);
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            rb.GetComponent<Movement>().AirborneFromRamp();
            FindObjectOfType<SlowMotion>().ActivateSlowMotion(0.5f, .75f);
            FindObjectOfType<ObjectMovement>().ignoreMultiplier = true;

        }
    }
}
