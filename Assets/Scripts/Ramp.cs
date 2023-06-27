using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour, IInteractable
{
    [SerializeField] float force, yStartPos;

    public void Interact(GameObject player)
    {
        var rb = player.GetComponent<Rigidbody>();
        rb.transform.position = new Vector3(rb.transform.position.x, yStartPos, rb.transform.position.z);
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
        player.GetComponent<Movement>().AirborneFromRamp();
        FindObjectOfType<SlowMotion>().ActivateSlowMotion(0.35f, .75f);
        FindObjectOfType<ObjectMovement>().ignoreMultiplier = true;
    }
}
