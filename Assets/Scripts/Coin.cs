using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IInteractable
{
    [SerializeField] float scoreValue = 10f;
    void IInteractable.Interact(GameObject player)
    {
        if (!FindObjectOfType<Score>()) { Debug.LogError("No 'score' object found!"); return; }
        if (!FindObjectOfType<CoinCollector>()) { Debug.LogError("No 'coin collector' object found!"); return; }

        FindObjectOfType<Score>().AddScore(scoreValue);
        FindObjectOfType<CoinCollector>().CollectCoin();
        Destroy(gameObject);
    }
}
