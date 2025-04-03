using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    public GameObject loseMenu;
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
        SceneManager.LoadScene("MapScene");
    }

    public void goMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
