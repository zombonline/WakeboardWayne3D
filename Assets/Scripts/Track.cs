using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
   public bool spawnDisabled = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ObstaclePrefab"))
        {
            spawnDisabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("ObstaclePrefab"))
        {
            spawnDisabled = false;
        }
    }

}
