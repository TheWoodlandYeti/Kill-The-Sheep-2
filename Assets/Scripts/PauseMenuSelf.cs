using System;
using UnityEngine;

public class PauseMenuSelf : MonoBehaviour
{
    [NonSerialized]
    public bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    readonly float timeScalePaused = 0f;
    readonly float timeScaleUnpaused = 1f;

    void Start()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Update()
    {
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
    }
    public void Resume()
    {
        Time.timeScale = timeScaleUnpaused;
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = timeScalePaused;
        gameIsPaused = true;
        pauseMenuUI.SetActive(true);
    }
}
