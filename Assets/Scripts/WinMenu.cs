using Codice.Client.BaseCommands;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Keith Chow A01405612
/// The WinMenu.
/// version 1.0
/// </summary>
public class WinMenu : MonoBehaviour
{
    public GameObject winMenu;
    public PlayerHealth playerHealth;
    public GameObject playUI;
    public AudioSource audioSource;
    public MapManager mapManager;
    
    /// <summary>
    /// Start method 
    /// </summary>
    void Start()
    {
        winMenu.SetActive(false);
        if (mapManager == null) mapManager = FindFirstObjectByType<MapManager>();
        if (mapManager == null) Debug.LogError("MapManager not found in the scene!", this);
        
    }

    /// <summary>
    /// GameWon method
    /// </summary>
    public void GameWon()
    {
        SafeTile endTile;

        if (mapManager == null)
        {
            Debug.LogError("MapManager is null in GameWon. Cannot calculate score.");
            winMenu.SetActive(true);
            Time.timeScale = 0f;
            return;
        }

        endTile = mapManager.GetEndTile();
        winMenu.SetActive(true);
        Time.timeScale = 0f;

        if (endTile != null)
        {
            int score;
            
            score = endTile.CalculateScore();
            audioSource.Play();

            if (GameManager.Instance != null)
            {
                PlayerHealth playerHealth;

                playerHealth = FindFirstObjectByType<PlayerHealth>();
                GameManager.Instance.UpdateCurrentHealth(playerHealth.GetHealth());
                GameManager.Instance.AppendWonScore(score);
                
                playUI.SetActive(false);
            }
            else
            {
                 Debug.LogError("GameManager Instance not found! Cannot save score.");
            }
        }
        else
        {
            Debug.LogError("End tile not found!");
        }
    }

    /// <summary>
    /// playAgain method
    /// </summary>
    public void playAgain()
    {
        Time.timeScale = 1f;
        
        if (GameManager.Instance != null)
        {
           GameManager.Instance.GoToNextLevel();
        }
        else
        {
           Debug.LogError("GameManager Instance not found! Cannot go to next level.");
           SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }

    /// <summary>
    /// goMainMenu method
    /// </summary>
    public void goMainMenu()
    {
        Time.timeScale = 1f;
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
