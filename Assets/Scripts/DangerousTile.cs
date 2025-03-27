// using System.Numerics;
using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents a Dangerous Tile
/// version 1.3
/// </summary>
public class DangerousTile : Tile
{
    GameObject spike;

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
        float spikeHeight;
        float tileHeight;
        float tileSize;
        Vector3 spikePosition;

        spikeSize = spikePrefab.transform.localScale.x;
        tileSize = this.transform.localScale.x;
        maxOffset = (tileSize - spikeSize) / 2;

        offsetX = Random.Range(-maxOffset, maxOffset);
        offsetY = Random.Range(-maxOffset, maxOffset);

        spikeHeight = spikePrefab.transform.localScale.y;
        tileHeight = 0.1141003f;

        // 0.2f + (0.1141003f/2)
        Debug.Log(spikeHeight);
        Debug.Log(tileHeight);

        spikePosition = new Vector3(
            GetPositionX() + offsetX, 
            (spikeHeight / 2) + (tileHeight / 2), 
            GetPositionY() + offsetY);

        spike = Instantiate(spikePrefab, spikePosition, Quaternion.identity);
        spike.transform.parent = this.transform;
    }
}
