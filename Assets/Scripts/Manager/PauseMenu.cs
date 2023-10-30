using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;

    [SerializeField] private GameObject pauseMenuUI;
    private GameManager gameManager;
    private SoundEffectsPlayer sfxPlayer;

    private void Start()
    {
        sfxPlayer = GetComponent<SoundEffectsPlayer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameManager.gameIsOver)
        {
            if (gameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        sfxPlayer.audioSrcMusic.volume = 0.56f;
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        gameIsPaused = false;
        sfxPlayer.audioSrc.UnPause();
    }

    public void Pause()
    {
        sfxPlayer.audioSrcMusic.volume = 0.25f;
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        gameIsPaused = true;
        sfxPlayer.audioSrc.Pause();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
