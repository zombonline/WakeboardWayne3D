using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;

    public void UpdateCoinsText()
    {
        coinsText.text = PlayerPrefs.GetInt(PlayerPrefsController.COINS_KEY).ToString("0000");
    }

}
