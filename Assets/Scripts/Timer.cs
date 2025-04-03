using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLimit = 60F;
    private float initialTimeLimit;
    [SerializeField] Text timerText;

    void Start()
    {
        initialTimeLimit = timeLimit;
    }

    void Update()
    {
        timeLimit -= Time.deltaTime;

        if(timeLimit <= 0)
        {
            timeLimit = 0;
            FindFirstObjectByType<LoseMenu>().GameLost();
        }

        int min = Mathf.FloorToInt(timeLimit / 60);
        int sec = Mathf.FloorToInt(timeLimit % 60);
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
