using UnityEngine;
using TMPro;  // Required for TextMeshPro

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;  // Reference to the TMP Text UI element
    private float timeRemaining = 90f;  // Starting time in seconds (90 seconds)
    private bool isTimerRunning = true;

    void Update()
    {
        // If the timer is running
        if (isTimerRunning)
        {
            // Subtract time from the remaining time
            timeRemaining -= Time.deltaTime;

            // Ensure the timer doesn't go below 0
            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;
                isTimerRunning = false;  // Stop the timer once it reaches 0
            }

            // Convert remaining time to minutes and seconds
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);

            // Update the UI text with the time in mm:ss format
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // Call this method to start the timer
    public void StartTimer()
    {
        isTimerRunning = true;
    }

    // Call this method to stop the timer
    public void StopTimer()
    {
        isTimerRunning = false;
    }
}
