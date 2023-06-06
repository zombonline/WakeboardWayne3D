using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] int coinsCollected = 0;

    [SerializeField] TextMeshProUGUI coinsCollectedText;

    [SerializeField] UnityEvent<float> onCoinsBanked;
    public void CollectCoin()
    {
        coinsCollected++;
        coinsCollectedText.text = coinsCollected.ToString("000");
    }

    public void BankCoins()
    {
        FindObjectOfType<PlayerPrefsController>().SetCoins(PlayerPrefs.GetInt(PlayerPrefsController.COINS_KEY) + coinsCollected);
        coinsCollected = 0;
        coinsCollectedText.text = coinsCollected.ToString("000");
        onCoinsBanked.Invoke(coinsCollected);
    }

}
