using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    [SerializeField] float force;
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            var rb = collision.GetComponent<Rigidbody>();

            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            rb.GetComponent<Movement>().AirborneFromRamp();
        }
    }
}
