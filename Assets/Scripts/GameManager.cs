using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Alex Choi
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int currentLevel = 0;
    public int currentGameScore = 0;
    public int currentHealth = 5;

    /// <summary>
    /// Awake method
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Start method
    /// </summary>
    public void StartNewGame()
    {
        currentLevel = 0;
        currentGameScore = 0;
        currentHealth = 5;
        GoToNextLevel();
    }

    /// <summary>
    /// Go to the next level
    /// </summary>
    public void GoToNextLevel()
    {
        currentLevel++;
        SceneManager.LoadSceneAsync("MapScene");
    }

    /// <summary>
    /// Resets the level
    /// </summary>
    public void ResetLevel()
    {
        currentLevel = 1;
        currentGameScore = 0;
    }

    public void ResetHealth()
    {
        currentHealth = 5;
    }

    /// <summary>
    /// Appends the won score
    /// </summary>
    /// <param name="newScore"></param>
    public void AppendWonScore(int newScore)
    {
        currentGameScore += newScore;
    }

    /// <summary>
    /// Update current health by new health value.
    /// </summary>
    /// <param name="newHealth"></param>
    public void UpdateCurrentHealth(int newHealth)
    {
        currentHealth = newHealth;
    }

    /// <summary>
    /// Returns to the main menu
    /// </summary>
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
