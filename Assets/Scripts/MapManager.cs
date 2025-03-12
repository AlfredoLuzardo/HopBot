using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject endingTilePrefab;
    public GameObject startingTilePrefab;
    public GameObject safeTilePrefab;
    public GameObject breakableTilePrefab;
    public GameObject dangerousTilePrefab;
    public GameObject playerObj;
    public GameObject enemyObj;
    public int rows = 10;
    public int cols = 10;
    public string difficulty = "easy";

    private Map map;

    public void Start()
    {
        map = new Map(rows, cols);
        map.GenerateMap(difficulty);
        DrawMap();
    }

    void DrawMap()
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
                    Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
                    
                    if (tilePrefab == startingTilePrefab)
                    {
                        Instantiate(playerObj, new Vector3(x, 1, y), Quaternion.identity);
                    }
                    else if (Random.Range(0, 100) > 95 && tilePrefab != endingTilePrefab)
                    {
                        Instantiate(enemyObj, new Vector3(x, 1, y), Quaternion.identity);
                    }
                }
            }
        }
    }
}
