using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
     public GameObject loseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loseMenu.SetActive(false);
    }

    public void GameLost()
    {
        loseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void playAgain()
    {
        Time.timeScale = 1f;
        // Debug.Log("Time scale set to 1");
        SceneManager.LoadScene("MapScene");
    }

    public void goMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
