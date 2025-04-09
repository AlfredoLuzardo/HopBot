using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Kieth Chow
/// Handles lose menu
/// </summary>
public class LoseMenu : MonoBehaviour
{
    public GameObject loseMenu;
    public GameObject playUI;
    public AudioSource audioSource;

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
        audioSource.Play();
        loseMenu.SetActive(true);
        playUI.SetActive(false);
        Time.timeScale = 0f;
    }

    /// <summary>
    /// play again method
    /// </summary>
    public void playAgain()
    {
        Time.timeScale = 1f;
        if(GameManager.Instance != null)
        {
            GameManager.Instance.ResetLevel();
            GameManager.Instance.ResetHealth();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Go main menu method
    /// </summary>
    public void goMainMenu()
    {
        Time.timeScale = 1f;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StopTheLevelMusic();
            GameManager.Instance.ReturnToMainMenu();
        }
        else
        {
            Debug.LogError("GameManager Instance not found! Cannot go to main menu.");
            SceneManager.LoadScene("MainMenu");
        }
    }
}
