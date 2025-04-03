using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    // Data to persist
    public int currentLevel = 0;
    public int currentGameScore = 0; // Consider if this should be total score or last level's score

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this GameObject persist across scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public void StartNewGame()
    {
        currentLevel = 0;
        currentGameScore = 0; // Reset score for a new game
        GoToNextLevel();
    }

    public void GoToNextLevel()
    {
        currentLevel++;
        // Reset score for the *new* level if needed, or keep accumulating
        // currentGameScore = 0; // Uncomment if score resets per level
        SceneManager.LoadSceneAsync("MapScene");
    }

    public void AppendWonScore(int newScore)
    {
        // Decide if you want to *add* to the score or *replace* it
        // currentGameScore += newScore; // To accumulate score across levels
        currentGameScore += newScore; // To store only the last won score
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Also stop play mode in editor
        #endif
    }
}
