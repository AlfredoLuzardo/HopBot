// using System.Numerics;
using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents a Dangerous Tile
/// version 1.1
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
        Vector3 spikePosition;

        offsetX = Random.Range(-0.4f, 0.4f);
        offsetY = Random.Range(-0.4f, 0.4f);

        spikePosition = new Vector3(GetPositionX() + offsetX, 0.5f, GetPositionY() + offsetY);

        spike = Instantiate(spikePrefab, spikePosition, Quaternion.identity);
        spike.transform.parent = this.transform;
    }
}
