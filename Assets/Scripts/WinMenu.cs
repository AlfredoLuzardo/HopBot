using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public GameObject winMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winMenu.SetActive(false);
    }

    public void GameWon()
    {
        winMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void playAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MapScene");
    }

    public void goMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
