using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AudioController audioController;

    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();
    }
    public void PlayGame()
    {
        audioController.PlaySFX(audioController.button);
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
