using UnityEngine;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents the behaviour of a ending tile
/// version 1.0
/// </summary>
public class EndingTileBehaviour : MonoBehaviour
{
    private SafeTile endingTile;

    public void SetEndingTile(SafeTile safeTile)
    {
        endingTile = safeTile;
    }

    /// <summary>
    /// Detects when the player collides with a safe tile
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && endingTile.GetIsEnd())
        {
            if(endingTile != null)
            {
                endingTile.EndRound();
            }
        }
    }
}
