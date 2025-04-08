using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Keith Chow
/// Handles the pause menu
/// </summary>
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject playUI;
    public GameObject loseMenu;
    public GameObject winMenu;
    public bool isPaused;
    
    /// <summary>
    /// Start method
    /// </summary>
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    /// <summary>
    /// Update Method
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !loseMenu.activeSelf && !winMenu.activeSelf)
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    /// <summary>
    /// PauseGame method
    /// </summary>
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        playUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    /// <summary>
    /// ResumeGame method
    /// </summary>
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        playUI.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    /// <summary>
    /// goMainMenu method=
    /// </summary>
    public void goMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
