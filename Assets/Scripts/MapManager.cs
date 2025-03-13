using UnityEngine;
using HopBotNamespace;

public class MapManager : MonoBehaviour
{
    public GameObject endingTilePrefab;
    public GameObject startingTilePrefab;
    public GameObject safeTilePrefab;
    public GameObject breakableTilePrefab;
    public GameObject dangerousTilePrefab;
    public GameObject playerObj;
    public GameObject playerInstance;
    public GameObject enemyObj;
    public int rows = 10;
    public int cols = 10;
    public string difficulty = "easy";

    private Map map;
    private Vector3 playerPos;

    public void Start()
    {
        map = new Map(rows, cols);
        map.GenerateMap(difficulty);
        DrawMap();
    }

    public GameObject GetPlayerInstance()
    {
        return playerInstance;
    }

    public Vector3 GetPlayerPos()
    {
        return playerPos;
    }

    public void UpdatePlayerMapPosition(Vector3 newPosition)
    {
        playerPos = newPosition;
        
        Debug.Log($"Player map position updated to: {playerPos}");
    }

    public void DrawMap()
    {
        for (int x = 0; x < map.GetNumRows(); x++)
        {
            for (int y = 0; y < map.GetNumCols(); y++)
            {
                GameObject tilePrefab = null;

                var tile = map[x, y];
                
                if (tile is SafeTile safeTile)
                {
                    if (safeTile.GetIsStart())
                    {
                        tilePrefab = startingTilePrefab;
                        Debug.Log("Starting Tile Created");
                        Debug.Log(tilePrefab);
                    }
                    else if (safeTile.GetIsEnd())
                    {
                        tilePrefab = endingTilePrefab;
                        Debug.Log("Ending Tile Created");
                        Debug.Log(tilePrefab);
                    }
                    else
                    {
                        tilePrefab = safeTilePrefab;
                    }
                    
                }
                else if (tile is BreakableTile)
                {
                    tilePrefab = breakableTilePrefab;
                }
                else if (tile is DangerousTile)
                {
                    tilePrefab = dangerousTilePrefab;
                }

                if (tilePrefab != null)
                {
                    // Generates Tile
                    Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
                    
                    // Generates Player and Enemies
                    if (tilePrefab == startingTilePrefab)
                    {
                        playerPos = new Vector3(x, 1, y);
                        playerInstance = Instantiate(playerObj, playerPos, Quaternion.identity);
                    }
                    else if (Random.Range(0, 100) > 95 && tilePrefab != endingTilePrefab)
                    {
                        Instantiate(enemyObj, new Vector3(x, 2, y), Quaternion.identity);
                    }
                }
            }
        }
    }
}
