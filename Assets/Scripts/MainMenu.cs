using UnityEngine;

/// <summary>
/// Author: Keith Chow
/// Handles main menu
/// </summary>
public class MainMenu : MonoBehaviour
{

    /// <summary>
    /// Plays the game
    /// </summary>
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
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.QuitGame();
        }
        else
        {
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
