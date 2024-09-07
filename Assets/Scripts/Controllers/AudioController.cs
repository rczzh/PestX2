using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    public AudioClip bgm;
    public AudioClip button;
    public AudioClip bulletFire;
    public AudioClip bulletHit;
    public AudioClip itemPickUp;

    private void Start()
    {
        //delete duplicate when traversing scene
        for (int i = 0; i < Object.FindObjectsOfType<AudioController>().Length; i++)
        {
            if (Object.FindObjectsOfType<AudioController>()[i] != this)
            {
                if (Object.FindObjectsOfType<AudioController>()[i].name == gameObject.name)
                {
                    Destroy(gameObject);
                }
            }

        }

        CheckAudioPreferences();

        DontDestroyOnLoad(gameObject);
        musicSource.clip = bgm;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip);
    }

    private void CheckAudioPreferences()
    {
        if(PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            AdjustMusicVolume();
        }
        if(PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            AdjustSFXVolume();
        }
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
}
