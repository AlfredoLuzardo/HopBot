using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Kieth Chow
/// Handles main menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    // public int currentLevel = 0;
    // public int currentGameScore = 0;
    public void PlayGame()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.StartNewGame();
        }
        else
        {
            Debug.LogError("GameManager instance not found.");
        }
        // SceneManager.LoadSceneAsync("MapScene");
    }

    // public void AppendWonScore(int newScore)
    // {
    //     currentGameScore = newScore;
    // }

    // public void IncrementLevel()
    // {
    //     currentLevel++;
    // }

    public void QuitGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.QuitGame();
        }
        else
        {
            // Fallback if GameManager doesn't exist for some reason
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
        // Application.Quit();
    }
}
