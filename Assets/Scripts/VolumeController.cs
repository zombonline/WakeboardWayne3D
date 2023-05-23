using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VolumeType
{
    SFX,
    BGM
}

public class VolumeController : MonoBehaviour
{
    [SerializeField] VolumeType volumeType;
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(volumeType == VolumeType.SFX) 
        {
            audioSource.volume = PlayerPrefs.GetFloat(PlayerPrefsController.SFX_VOLUME_KEY);
        }
        else if(volumeType == VolumeType.BGM)
        {
            audioSource.volume = PlayerPrefs.GetFloat(PlayerPrefsController.BGM_VOLUME_KEY);

        }
    }
}
