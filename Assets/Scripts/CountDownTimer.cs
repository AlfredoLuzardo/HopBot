using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Author: Alex Choi A01323994
/// Countdown timer that counts from 3 to Go with font growth and fade-out
/// References: ChatGPT, and Unity documentation
/// </summary>
public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private Text countdownText;
    private float startFontSize = 100f;
    private float endFontSize = 150f;
    private float totalDuration = 1f;

    /// <summary>
    /// Initializes the countdown timer and starts the countdown coroutine.
    /// </summary>
    private void Start()
    {
        StartCoroutine(StartCountdown());
    }

    /// <summary>
    /// Starts the Countdown
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartCountdown()
    {
        float elapsed;
        float fadeDuration;
        float fadeElapsed;

        elapsed = 0f;

        while (elapsed < totalDuration)
        {
            float remaining;
            float t;
            float fontSize;
            
            
            elapsed += Time.unscaledDeltaTime;

            remaining               = Mathf.Clamp(totalDuration - elapsed, 0f, totalDuration);
            countdownText.text      = remaining.ToString("F2");
            t                       = elapsed / totalDuration;
            fontSize                = Mathf.Lerp(startFontSize, endFontSize, t * t);
            countdownText.fontSize  = Mathf.FloorToInt(fontSize);
            countdownText.color     = new Color(1f, 1f, 1f, 1f);

            yield return null;
        }

        countdownText.text      = "Go";
        countdownText.fontSize  = Mathf.FloorToInt(endFontSize);
        countdownText.color     = new Color(1f, 1f, 1f, 1f);

        yield return new WaitForSecondsRealtime(0.5f);

        fadeDuration = 0.5f;
        fadeElapsed  = 0f;

        while (fadeElapsed < fadeDuration)
        {
            float alpha;
            
            fadeElapsed += Time.unscaledDeltaTime;
            
            alpha               = Mathf.Lerp(1f, 0f, fadeElapsed / fadeDuration);
            countdownText.color = new Color(1f, 1f, 1f, alpha);
            
            yield return null;
        }

        countdownText.gameObject.SetActive(false);
    }
}
