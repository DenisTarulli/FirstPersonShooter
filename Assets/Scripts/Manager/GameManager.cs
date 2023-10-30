using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float startingTime; // en segundos
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject WinText;
    [SerializeField] private GameObject LoseText;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private SoundEffectsPlayer sfxPlayer;

    private PlayerActions player;
    private const string IS_PLAYER = "Player";

    private float remainingTime;
    [HideInInspector] public bool gameIsOver = false;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void Start()
    {        
        player = GameObject.FindWithTag(IS_PLAYER).GetComponent<PlayerActions>();
        Cursor.lockState = CursorLockMode.Locked;
        remainingTime = startingTime;
        gameOverUI.SetActive(false);
    }

    private void Update()
    {
        TimeUpdate();

        if ((player.currentHealth <= 0 || remainingTime <= 1) && !gameIsOver)
            GameOver();
    }

    private void TimeUpdate()
    {        
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (remainingTime <= 10)
            timerText.color = Color.red;
    }

    private void GameOver()
    {
        pauseMenu.gameIsPaused = true;
        gameIsOver = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0f;
        gameOverUI.SetActive(true);

        if (player.currentHealth <= 0)
        {
            LoseText.SetActive(true);
            sfxPlayer.LoseSound();
        }
        else
        {
            WinText.SetActive(true);
            sfxPlayer.WinSound();
        }
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
