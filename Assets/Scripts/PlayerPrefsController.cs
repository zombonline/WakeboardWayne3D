using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerPrefsController : MonoBehaviour
{
    #region SETTINGS
    public const string SFX_VOLUME_KEY = "sfx_volume";
    public const float DEFAULT_SFX_VOLUME_KEY = .75f;
    public const string BGM_VOLUME_KEY = "bgm_volume";
    public const float DEFAULT_BGM_VOLUME_KEY = .75f;
    #endregion

    #region CUSTOMISATION
    public const string COSTUME_ID_KEY = "costume_id";
    public const string BOARD_ID_KEY = "board_id";
    public const string PET_ID_KEY = "pet_id";

    [SerializeField] GameObject[] wayneArray, boardArray;

    #endregion

    #region SCORE AND CURRENCY
    public const string COINS_KEY = "coins";
    public const string HIGH_SCORE_KEY = "high_score";

    #endregion

    private void Awake()
    {
        AssignModels();
    }

    


    #region SETTINGS METHODS
    public void SetSFXVolume(System.Single volume = -1f, float volOverride = -1f)
    {
        if(volume != -1f)
        {
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
        }
        else if(volOverride != -1f)
        {
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volOverride);
        }
    }

    public void SetBGMVolume(System.Single volume = -1f, float volOverride = -1f)
    {
        if (volume != -1f)
        {
            PlayerPrefs.SetFloat(BGM_VOLUME_KEY, volume);
        }
        else if (volOverride != -1f)
        {
            PlayerPrefs.SetFloat(BGM_VOLUME_KEY, volOverride);
        }
    }


    public void SetSliderValues()
    {
        foreach (Slider slider in FindObjectsOfType<Slider>()) 
        {
            if (slider.name == "Slider (SFX)")
            {
                slider.value = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, DEFAULT_SFX_VOLUME_KEY);
            }
            else if (slider.name == "Slider (BGM)")
            {
                slider.value = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, DEFAULT_BGM_VOLUME_KEY);

            }
        }
    }
    #endregion

    #region CUSTOMISATION METHODS

    public void SetCustomisation(ShopItem shopItem)
    {
        if (shopItem.itemType == ItemType.Costume) { PlayerPrefs.SetInt(COSTUME_ID_KEY, shopItem.ItemID); }
        else if (shopItem.itemType == ItemType.Board) { PlayerPrefs.SetInt(BOARD_ID_KEY, shopItem.ItemID); }
        else if (shopItem.itemType == ItemType.Pet) { PlayerPrefs.SetInt(PET_ID_KEY, shopItem.ItemID); }
    }

    public void SetCostume(int costume)
    {
        PlayerPrefs.SetInt(COSTUME_ID_KEY, costume);
    }
    public void SetBoard(int board)
    {
        PlayerPrefs.SetInt(BOARD_ID_KEY, board);
    }
    public void SetPet(int pet)
    {
        PlayerPrefs.SetInt(PET_ID_KEY, pet);
    }

    public void AssignModels()
    {
        var wayne = GameObject.FindWithTag("Player Model");
        ShowModel(wayneArray, wayne, COSTUME_ID_KEY);
        var board = GameObject.FindWithTag("Board Model");
        ShowModel(boardArray, board, BOARD_ID_KEY);
        var pet = GameObject.FindWithTag("Pet Model");
    }

    private void ShowModel(GameObject[] modelArray, GameObject modelPos, string key)
    {
        if (modelPos.transform.childCount > 0)
        {
            for (int i = modelPos.transform.childCount -1; i >= 0; i--)
            {
                if(modelPos.transform.GetChild(i))
                {
                    Destroy(modelPos.transform.GetChild(i).gameObject);
                }
            }
        }


        var newModel = Instantiate(modelArray[PlayerPrefs.GetInt(key, 0)]);

        newModel.transform.parent = modelPos.transform;
        newModel.transform.localPosition = Vector3.zero;
        newModel.transform.localEulerAngles = Vector3.zero;
    }
    #endregion

    #region SCORE AND CURRENCY METHODS

    public void SetCoins(int coins)
    {
        PlayerPrefs.SetInt(COINS_KEY, coins);
    }

    public void SetHighScore(float highScore)
    {
        PlayerPrefs.SetFloat(HIGH_SCORE_KEY, highScore);
    }

    #endregion

}
