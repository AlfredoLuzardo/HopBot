using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Alex Choi
/// Handles timer
/// version 1.0
/// </summary>
public class Timer : MonoBehaviour
{
    public float timeLimit = 60F;
    private float initialTimeLimit;
    [SerializeField] Text timerText;

    /// <summary>
    /// Start method
    /// </summary>
    void Start()
    {
        initialTimeLimit = timeLimit;
    }

    /// <summary>
    /// Update method
    /// </summary>
    void Update()
    {
        int min;
        int sec;

        timeLimit -= Time.deltaTime;

        if(timeLimit <= 0)
        {
            timeLimit = 0;
            FindFirstObjectByType<LoseMenu>().GameLost();
        }

        min = Mathf.FloorToInt(timeLimit / 60);
        sec = Mathf.FloorToInt(timeLimit % 60);
        timerText.text = string.Format("Timer {0:00}:{1:00}", min, sec);
    }


    /// <summary>
    /// Getter for the remaining time
    /// </summary>
    /// <returns></returns>
    public float GetRemainingTime()
    {
        return timeLimit;
    }

    /// <summary>
    /// Getter for the initial time
    /// </summary>
    /// <returns></returns>
    public float GetInitialTimeLimit()
    {
        return initialTimeLimit;
    }
}
