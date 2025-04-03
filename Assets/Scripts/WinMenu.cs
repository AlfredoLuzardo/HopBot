using Codice.Client.BaseCommands;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public GameObject winMenu;
    void Start()
    {
        winMenu.SetActive(false);
    }

    public void GameWon()
    {
        SafeTile endTile = null;
        winMenu.SetActive(true);
        Time.timeScale = 0f;
        MainMenu mainMenu = GetComponent<MainMenu>();

        Tile[,] currentMap = GetComponent<MapManager>().GetMap().GetMap();

        for (int x = 0; x < currentMap.GetLength(0); x++)
        {
            for (int y = 0; y < currentMap.GetLength(1); y++)
            {
                if (currentMap[x, y] is SafeTile safeTile)
                {
                    if (safeTile.GetIsEnd())
                    {
                        endTile = safeTile;
                        break;
                    }
                }
            }
            if (endTile != null) break;
        }

        if (endTile != null)
        {
            int score = endTile.CalculateScore();
            mainMenu.AppendWonScore(score);
        }
        else
        {
            Debug.LogError("End tile not found!");
        }
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
