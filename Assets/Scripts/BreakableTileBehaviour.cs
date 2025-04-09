using System.Collections;
using UnityEngine;

namespace HopBotNamespace
{
    /// <summary>
    /// Author: Alfredo Luzardo A01379913
    /// Represents the behaviour of a breakable tile
    /// version 1.1
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
                    GameObject tileObject;
                    Animator anim;

                    tileObject = breakableTile.GetTileObject();
                    anim = tileObject.GetComponent<Animator>();
                    // breakableTile.audioSource.Play();

                    breakableTile.DecrementDurability();
                    
                    if(anim != null)
                    {
                        anim.enabled = true;
                        StartCoroutine(WaitForAnimation(anim));
                    }
                }
            }
        }

        /// <summary>
        /// Waits for the animation to play
        /// </summary>
        /// <param name="anim"></param>
        /// <returns></returns>
        private IEnumerator WaitForAnimation(Animator anim)
        {
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            anim.enabled = false;
        }
    }
}
