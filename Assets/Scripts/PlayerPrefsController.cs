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

    private void Awake()
    {
        AssignModels();
    }


    #region SETTINGS METHODS
    public void SetSFXVolume(System.Single volume)
    {
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
    }

    public void SetBGMVolume(System.Single volume)
    {
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, volume);
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
        var board = GameObject.FindWithTag("Board Model");
        var pet = GameObject.FindWithTag("Pet Model");
        ShowModel(wayneArray, wayne, COSTUME_ID_KEY);
        ShowModel(boardArray, board, BOARD_ID_KEY);
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
}
