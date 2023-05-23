using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerPrefsController : MonoBehaviour
{

    public const string SFX_VOLUME_KEY = "sfx_volume";
    public const float DEFAULT_SFX_VOLUME_KEY = .75f;
    public const string BGM_VOLUME_KEY = "bgm_volume";
    public const float DEFAULT_BGM_VOLUME_KEY = .75f;

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


}
