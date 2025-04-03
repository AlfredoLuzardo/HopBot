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
        // SafeTile endTile = null;
        // winMenu.SetActive(true);
        // Time.timeScale = 0f;
        // MainMenu mainMenu = GetComponent<MainMenu>();

        // Tile[,] currentMap = GetComponent<MapManager>().GetMap().GetMap();

        // for (int x = 0; x < currentMap.GetLength(0); x++)
        // {
        //     for (int y = 0; y < currentMap.GetLength(1); y++)
        //     {
        //         if (currentMap[x, y] is SafeTile safeTile)
        //         {
        //             if (safeTile.GetIsEnd())
        //             {
        //                 endTile = safeTile;
        //                 break;
        //             }
        //         }
        //     }
        //     if (endTile != null) break;
        // }

        // if (endTile != null)
        // {
        //     int score = endTile.CalculateScore();
        //     mainMenu.AppendWonScore(score);
        // }
        // else
        // {
        //     Debug.LogError("End tile not found!");
        // }
        if (mapManager == null)
        {
             Debug.LogError("MapManager is null in GameWon. Cannot calculate score.");
             // Optionally still show win menu but without score logic
             winMenu.SetActive(true);
             Time.timeScale = 0f;
             return;
        }

        SafeTile endTile = null;
        winMenu.SetActive(true);
        Time.timeScale = 0f;

        // Remove: MainMenu mainMenu = GetComponent<MainMenu>(); // This was the problem

        Tile[,] currentMap = mapManager.GetMap().GetMap(); // Get map from MapManager

        // Loop to find the end tile (your existing logic is fine here)
        for (int x = 0; x < currentMap.GetLength(0); x++)
        {
            for (int y = 0; y < currentMap.GetLength(1); y++)
            {
                if (currentMap[x, y] is SafeTile safeTile && safeTile.GetIsEnd())
                {
                    endTile = safeTile;
                    break;
                }
            }
            if (endTile != null) break;
        }

        if (endTile != null)
        {
            int score = endTile.CalculateScore();
            // Access the GameManager Singleton to store the score
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AppendWonScore(score);
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
        // SceneManager.LoadScene("MapScene");
        if (GameManager.Instance != null)
         {
             GameManager.Instance.GoToNextLevel();
         }
         else
         {
             Debug.LogError("GameManager Instance not found! Cannot go to next level.");
             // Fallback: Just reload current scene?
             SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current map scene
         }
    }

    /// <summary>
    /// goMainMenu method
    /// </summary>
    public void goMainMenu()
    {
        Time.timeScale = 1f;
        // SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
         // Ask GameManager to return to main menu
         if (GameManager.Instance != null)
         {
             GameManager.Instance.ReturnToMainMenu();
         }
         else
         {
              Debug.LogError("GameManager Instance not found! Cannot go to main menu.");
              // Fallback: Load main menu directly
              SceneManager.LoadScene("MainMenu");
         }
    }
}
