using UnityEngine;
using HopBotNamespace;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Manages a Map
/// version 1.5
/// </summary>
public class MapManager : MonoBehaviour
{
    public GameObject endingTilePrefab;
    public GameObject startingTilePrefab;
    public GameObject safeTilePrefab;
    public GameObject breakableTilePrefab;
    public GameObject dangerousTilePrefab;
    public GameObject spikePrefab;
    public GameObject playerObj;
    public GameObject playerInstance;
    public GameObject enemyObj;
    public GameObject invincibilityItem;
    public GameObject stopEnemiesItem;
    public int rows = 10;
    public int cols = 10;
    public string difficulty = "easy";

    private Map map;
    private Vector3 playerPos;

    /// <summary>
    /// Start initializes the instance vars
    /// </summary>
    public void Start()
    {
        Time.timeScale = 1f;
        map = new Map(rows, cols);
        map.GenerateMap(difficulty);
        DrawMap();
    }

    /// <summary>
    /// Gets the instance of the player
    /// </summary>
    /// <returns></returns>
    public GameObject GetPlayerInstance() => playerInstance;

    /// <summary>
    /// Gets the player position
    /// </summary>
    /// <returns></returns>
    public Vector3 GetPlayerPos() => playerPos;

    /// <summary>
    /// Gets the map
    /// </summary>
    /// <returns></returns>
    public Map GetMap() => map;

    /// <summary>
    /// Updates the players instance position
    /// </summary>
    /// <param name="newPosition"></param>
    void Update()
    {
        playerPos = transform.position;
    }

    /// <summary>
    /// Builds the map in 3D using different Tiles
    /// Also spawns the player, and enemies
    /// </summary>
    private void DrawMap()
    {
        for (int x = 0; x < map.GetNumRows(); x++)
        {
            for (int y = 0; y < map.GetNumCols(); y++)
            {
                GameObject tilePrefab;
                Tile tile;
                
                tile = map[x, y];
                tilePrefab = GetTilePrefab(tile);

                if (tilePrefab != null)
                {
                    GameObject newTile;
                    
                    newTile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
                    HandleTileBehaviours(tile, newTile, x, y);
                }
            }
        }
    }

    /// <summary>
    /// Gets the tile prefab based on the type of the tile
    /// </summary>
    /// <param name="tile"></param>
    /// <returns></returns>
    private GameObject GetTilePrefab(Tile tile)
    {
        GameObject prefab;

        prefab = null;

        if (tile is SafeTile safeTile)
        {
            if(safeTile.GetIsStart())
            {
                prefab = startingTilePrefab;
            }
            else if (safeTile.GetIsEnd())
            {
                prefab = endingTilePrefab;
            }
            else
            {
                prefab = safeTilePrefab;
            }
        }
        else if (tile is BreakableTile)
        {
            prefab = breakableTilePrefab;
        }
        else if (tile is DangerousTile)
        {
            prefab = dangerousTilePrefab;
        }

        return prefab;
    }

    /// <summary>
    /// Handles the behaviour of different tiles
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="newTile"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void HandleTileBehaviours(Tile tile, GameObject newTile, int x, int y)
    {
        if(tile is SafeTile safeTile)
        {
            if(safeTile.GetIsStart())
            {
                playerPos = new Vector3(x, 1, y);
                playerInstance = Instantiate(playerObj, playerPos, Quaternion.identity);
            }
            else if(safeTile.GetIsEnd())
            {
                EndingTileBehaviour tileBehaviour;

                tileBehaviour = newTile.AddComponent<EndingTileBehaviour>();
                tileBehaviour.SetEndingTile(safeTile);
            }
            else
            {
                float num;

                num = Random.Range(0, 1000);

                if(num > 950)
                {
                    safeTile.SpawnItem(invincibilityItem);
                }
                else if(num > 900)
                {
                    safeTile.SpawnItem(stopEnemiesItem);
                }
            }
        }
        else if(tile is DangerousTile dangerousTile)
        {
            if (Random.Range(0, 100) > 70 )
            {
                dangerousTile.SpawnEnemy(enemyObj);
            }
            else
            {
                dangerousTile.SpawnSpike(spikePrefab);
                dangerousTile.GetSpike().AddComponent<SpikeBehaviour>();
            }
        }
        else if(tile is BreakableTile breakableTile)
        {
            BreakableTileBehaviour tileBehaviour;

            breakableTile.setTileObject(newTile);
            tileBehaviour = newTile.AddComponent<BreakableTileBehaviour>();
            tileBehaviour.SetBreakableTile(breakableTile);
        }
    }
}
