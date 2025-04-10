using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents a Dangerous Tile
/// version 1.3
/// </summary>
public class DangerousTile : Tile
{
    GameObject spike;
    const float TILE_HEIGHT = 0.1141003f;

    /// <summary>
    /// Getter for Spike
    /// </summary>
    /// <returns>spike</returns>
    public GameObject GetSpike()
    {
        return this.spike;
    }

    /// <summary>
    /// Spawns a spike on the tile
    /// </summary>
    /// <param name="spikePrefab"></param>
    public void SpawnSpike(GameObject spikePrefab)
    {
        float offsetX;
        float offsetY;
        float maxOffset;
        float spikeSize;
        float tileSize;
        Vector3 spikePosition;

        spikeSize = spikePrefab.transform.localScale.x;
        tileSize = this.transform.localScale.x;
        maxOffset = (tileSize - spikeSize * 0.5f) / 2;

        offsetX = Random.Range(-maxOffset, maxOffset);
        offsetY = Random.Range(-maxOffset, maxOffset);

        spikePosition = new Vector3(
            GetPositionX() + offsetX, 
            0.3f + TILE_HEIGHT / 2, 
            GetPositionY() + offsetY);

        spike = Instantiate(spikePrefab, spikePosition, Quaternion.identity);
        spike.transform.parent = this.transform;
    }

    /// <summary>
    /// Spawns an enemy
    /// </summary>
    /// <param name="enemyPrefab"></param>
    public void SpawnEnemy(GameObject enemyPrefab)
    {
        float enemyHeight;
        enemyHeight = enemyPrefab.transform.localScale.y;

        Instantiate(enemyPrefab, 
                    new Vector3(
                        GetPositionX(), 
                        (enemyHeight / 2) + (TILE_HEIGHT / 2), 
                        GetPositionY()), 
                    Quaternion.identity);
    }
}
