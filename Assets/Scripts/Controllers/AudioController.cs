using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

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

        DontDestroyOnLoad(gameObject);
        musicSource.clip = bgm;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip);
    }
}
