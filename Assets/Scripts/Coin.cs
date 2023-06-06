using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float scoreValue = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!FindObjectOfType<Score>()) { Debug.LogError("No 'score' object found!"); return; }
            if(!FindObjectOfType<CoinCollector>()) { Debug.LogError("No 'coin collector' object found!"); return; }

            FindObjectOfType<Score>().AddScore(scoreValue);
            FindObjectOfType<CoinCollector>().CollectCoin();
            Destroy(gameObject);
        }
    }
}
