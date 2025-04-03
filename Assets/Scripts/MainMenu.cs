using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Kieth Chow
/// Handles main menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    public int currentLevel = 0;
    public int currentGameScore = 0;
    public void PlayGame()
    {
        IncrementLevel();
        SceneManager.LoadSceneAsync("MapScene");
    }

    public void AppendWonScore(int newScore)
    {
        currentGameScore = newScore;
    }

    public void IncrementLevel()
    {
        currentLevel++;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
