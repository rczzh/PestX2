using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenues : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public bool playerIsDead = false;

    public GameObject pauseMenuUI;
    public GameObject deathMenuUI;
    public GameObject victoryMenuUI;

    // Update is called once per frame
    void Update()
    {
        // pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        // death
        if (playerIsDead == true)
        {
            deathMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartingScene");
    }

    public void Restart()
    {
        playerIsDead = false;
        // resumes the game
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}
