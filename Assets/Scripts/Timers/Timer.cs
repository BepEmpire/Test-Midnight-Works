using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("UI Configuration")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    [SerializeField] private float duration = 120f;
    
    [Header("Dependencies")]
    [SerializeField] private GameOverPopup gameOverPopup;
    [SerializeField] private CarSpawner carSpawner;

    private float _remainingTime;
    private bool _isRunning = false;

    private void Start()
    {
        ResetTimer();
        StartTimer();
    }

    private void Update()
    {
        if (_isRunning)
        {
            _remainingTime -= Time.deltaTime;

            if (_remainingTime <= 0)
            {
                _remainingTime = 0;
                _isRunning = false;

                TriggerGameOver();
            }

            UpdateTimerUI();
        }
    }

    private void StartTimer()
    {
        _remainingTime = duration;
        _isRunning = true;
    }

    public void StopTimer()
    {
        _isRunning = false;
    }

    public void ResetTimer()
    {
        _remainingTime = duration;
        _isRunning = false;
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = GetFormattedTime(_remainingTime);
        }
    }

    private string GetFormattedTime(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        return timeSpan.ToString("mm\\:ss");
    }

    private void TriggerGameOver()
    {
        carSpawner.DisableCurrentCarControl();
        
        int finalScore = FindObjectOfType<CarController>().GetDriftScore();
        gameOverPopup.ShowPopup(finalScore);
    }
}