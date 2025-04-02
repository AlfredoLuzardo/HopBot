using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents the StopEnemies item
/// References: https://docs.unity3d.com/6000.0/Documentation/ScriptReference/MonoBehaviour.StartCoroutine.html
///             https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Coroutine.html
/// version 1.1
/// </summary>
public class StopEnemies : Item
{
    private float duration = 2f;
    private Collider itemCollider;
    private MeshRenderer itemRenderer;

    /// <summary>
    /// Start method
    /// </summary>
    private void Start()
    {
        Transform child;

        child = transform.GetChild(0);
        itemCollider = GetComponent<Collider>();
        
        if(child != null)
        {
            itemRenderer = child.GetComponent<MeshRenderer>();
        }
    }

    /// <summary>
    /// Detects when the player collides with the StopEnemies item.
    /// </summary>
    /// <param name="collision"></param>
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EnemyBehaviour[] enemies;

            enemies = FindObjectsOfType<EnemyBehaviour>();

            foreach(EnemyBehaviour enemy in enemies)
            {
                enemy.SetNotAllowed();
            }

            StartCoroutine(ResumeEnemies(enemies));

            itemCollider.enabled = false;
            itemRenderer.enabled = false;
        }
    }

    /// <summary>
    /// Yields for the number of seconds, then resumes the enemies
    /// </summary>
    /// <param name="enemies"></param>
    /// <returns></returns>
    private IEnumerator ResumeEnemies(EnemyBehaviour[] enemies)
    {
        yield return new WaitForSeconds(duration);

        foreach(EnemyBehaviour enemy in enemies)
        {
            enemy.SetAllowed();
        }
    }
}
