using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Alex Choi
/// Represents the score
/// version 1.0
/// </summary>
public class Score : MonoBehaviour
{
    public Text scoreText; 

    /// <summary>
    /// Update method
    /// </summary>
    void Update()
    {
        if (scoreText != null && GameManager.Instance != null)
        {
            scoreText.text = "Score: " + GameManager.Instance.currentGameScore;
        }
         else if (scoreText == null)
        {
            Debug.LogError("Score Text component not assigned!", this);
        }
    }
}
