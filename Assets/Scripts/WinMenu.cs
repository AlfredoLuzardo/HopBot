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
    
    /// <summary>
    /// Start method 
    /// </summary>
    void Start()
    {
        winMenu.SetActive(false);
    }

    /// <summary>
    /// GameWon method
    /// </summary>
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

    /// <summary>
    /// playAgain method
    /// </summary>
    public void playAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MapScene");
    }

    /// <summary>
    /// goMainMenu method
    /// </summary>
    public void goMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
