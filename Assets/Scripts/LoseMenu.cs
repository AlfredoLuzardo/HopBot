using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Kieth Chow
/// Handles lose menu
/// </summary>
public class LoseMenu : MonoBehaviour
{
    public GameObject loseMenu;

    /// <summary>
    /// Start method
    /// </summary>
    void Start()
    {
        loseMenu.SetActive(false);
    }

    /// <summary>
    /// Game Lost method
    /// </summary>
    public void GameLost()
    {
        loseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    /// <summary>
    /// play again method
    /// </summary>
    public void playAgain()
    {
        Time.timeScale = 1f;
        // SceneManager.LoadScene("MapScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Go main menu method
    /// </summary>
    public void goMainMenu()
    {
        Time.timeScale = 1f;
        // SceneManager.LoadScene("MainMenu");
        if (GameManager.Instance != null)
         {
             GameManager.Instance.ReturnToMainMenu();
         }
         else
         {
              Debug.LogError("GameManager Instance not found! Cannot go to main menu.");
              // Fallback: Load main menu directly
              SceneManager.LoadScene("MainMenu");
         }
    }
}
