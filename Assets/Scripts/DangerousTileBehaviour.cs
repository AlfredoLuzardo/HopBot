using UnityEngine;

public class DangerousTileBehaviour : MonoBehaviour
{
    public DangerousTile dangerousTile;
    GameObject spike;

    /// <summary>
    /// Sets the DangerousTile reference.
    /// </summary>
    public void SetDangerousTile(DangerousTile tile)
    {
        dangerousTile = tile;
    }

    /// <summary>
    /// Getter for Spike
    /// </summary>
    /// <returns>spike</returns>
    public GameObject GetSpike()
    {
        return this.spike;
    }

    public void SpawnSpike(GameObject spikePrefab)
    {
        float offsetX;
        float offsetY;
        Vector3 spikePosition;

        offsetX = Random.Range(-0.4f, 0.4f);
        offsetY = Random.Range(-0.4f, 0.4f);

        spikePosition = new Vector3(dangerousTile.GetPositionX() + offsetX, 0.5f, dangerousTile.GetPositionY() + offsetY);

        spike = Instantiate(spikePrefab, spikePosition, Quaternion.identity);
        spike.transform.parent = this.transform;
    }
}
