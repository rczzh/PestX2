using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    private void Start()
    {
        CheckAudioPreferences();
    }

    public void AdjustMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void AdjustSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void CheckAudioPreferences()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            AdjustMusicVolume();
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            AdjustSFXVolume();
        }
    }
}
