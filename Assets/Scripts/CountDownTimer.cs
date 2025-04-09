using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Countdown timer that counts from 3 to Go with font growth and fade-out
/// </summary>
public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private Text countdownText;
    private float startFontSize = 100f;
    private float endFontSize = 150f;

    private void Start()
    {
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            countdownText.color = new Color(1f, 1f, 1f, 1f);

            float elapsed = 0f;
            while (elapsed < 1f)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = elapsed / 1f;
                float fontSize = Mathf.Lerp(startFontSize, endFontSize, t * t);
                countdownText.fontSize = Mathf.FloorToInt(fontSize);
                yield return null;
            }

            countdownText.fontSize = Mathf.FloorToInt(startFontSize);
        }

        countdownText.text = "Go";
        countdownText.fontSize = Mathf.FloorToInt(endFontSize);
        countdownText.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.5f);

        float fadeDuration = 0.5f;
        float fadeElapsed = 0f;
        while (fadeElapsed < fadeDuration)
        {
            fadeElapsed += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(1f, 0f, fadeElapsed / fadeDuration);
            countdownText.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        countdownText.gameObject.SetActive(false);
    }
}
