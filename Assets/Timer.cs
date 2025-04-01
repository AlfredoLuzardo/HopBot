using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLimit = 90F;
    [SerializeField] Text timerText;

    void Update()
    {
        timeLimit -= Time.deltaTime;

        if(timeLimit <= 0)
        {
            timeLimit = 0;
        }

        int min = Mathf.FloorToInt(timeLimit / 60);
        int sec = Mathf.FloorToInt(timeLimit % 60);
        timerText.text = string.Format("Timer {0:00}:{1:00}", min, sec);
    }
}
