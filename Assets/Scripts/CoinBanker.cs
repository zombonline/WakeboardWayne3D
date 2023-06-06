using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBanker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!FindObjectOfType<CoinCollector>()) { Debug.LogError("No 'coin collector' item found"); }
            FindObjectOfType<CoinCollector>().BankCoins();
            Destroy(gameObject);
        }
    }
}
