/*
 * Description: Kills the player when they enter dangerous areas such as water or lava.
 */

using UnityEngine;

/// Hazard area that causes player death.
public class Hazard : MonoBehaviour
{
    /// Message shown when player dies.
    [SerializeField] private string deathMessage = "You died.";

    /// Kills the player when they enter the hazard.
    /// <param name="other">Object that entered the hazard.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.PlayerDied(deathMessage);
        }
    }
}