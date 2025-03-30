using UnityEngine;


/// <summary>
/// Author: Alfredo Luzardo A01379913
/// Represents something that is harmful
/// version 1.0
/// </summary>
public interface Harmful
{
    /// <summary>
    /// Attacks the player
    /// </summary>
    /// <param name="player"></param>
    void attack(GameObject player);
}
