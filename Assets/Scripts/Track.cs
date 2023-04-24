using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public bool trackOccupied;
    public bool railingActive;
    public void DelayTrackSpawning(float length)
    {
        StartCoroutine(TrackDelay(length));
    }

    IEnumerator TrackDelay(float length)
    {
        trackOccupied = true;
        yield return new WaitForSeconds(length);
        trackOccupied= false;
    }

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
