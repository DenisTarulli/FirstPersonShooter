using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float startingTime; // en segundos
    private float remainingTime;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        remainingTime = startingTime;
    }

    private void Update()
    {
        TimeUpdate();
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
}
