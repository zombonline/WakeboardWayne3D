using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPiggyBank : MonoBehaviour, IInteractable
{
    public void Interact(GameObject player)
    {
        if (!FindObjectOfType<CoinCollector>()) { Debug.LogError("No 'coin collector' item found"); }
        FindObjectOfType<CoinCollector>().BankCoins();
        Destroy(gameObject);
    }
}
