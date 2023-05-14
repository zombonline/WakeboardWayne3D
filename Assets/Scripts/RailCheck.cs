using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailCheck : MonoBehaviour
{
    public bool railingActive;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Railing"))
        {
            railingActive= true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Railing"))
        {
            railingActive= false;
        }
    }
}
