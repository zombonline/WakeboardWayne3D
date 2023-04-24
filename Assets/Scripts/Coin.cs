using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<Score>().AddScore(10f);
            Destroy(gameObject);
        }
    }
}
