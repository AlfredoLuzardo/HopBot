using UnityEngine;

namespace HopBotNamespace
{
    /// <summary>
    /// Author: Alfredo Luzardo A01379913
    /// Represents the behaviour of a breakable tile
    /// version 1.0
    /// </summary>
    public class BreakableTileBehaviour : MonoBehaviour
    {
        private BreakableTile breakableTile;

        /// <summary>
        /// Sets the BreakableTile reference.
        /// </summary>
        public void SetBreakableTile(BreakableTile tile)
        {
            breakableTile = tile;
        }

        /// <summary>
        /// Detects when the player collides with the tile.
        /// </summary>
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (breakableTile != null)
                {
                    breakableTile.DecrementDurability();
                }
            }
        }
    }
}
