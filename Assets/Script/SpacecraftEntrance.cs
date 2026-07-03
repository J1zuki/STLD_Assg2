/*
 * Description: Lets player escape after all collectibles are collected.
 */

using UnityEngine;

/// Spacecraft entrance that checks if player can take off.
public class SpacecraftEntrance : InteractableObject
{
    /// Shows correct prompt based on collectible progress.
    /// <returns>Prompt message.</returns>
    public override string GetPromptMessage()
    {
        if (GameManager.instance.HasAllCollectibles())
        {
            return "Press E to take off.";
        }

        return "Find all repair parts first.";
    }

    /// Checks collectibles and ends game if ready.
    public override void Interact()
    {
        if (GameManager.instance.HasAllCollectibles())
        {
            GameManager.instance.CompleteGame();
        }
        else
        {
            GameManager.instance.ShowMessage("Missing parts. Collect scrap, energy coil, and fuel.");
        }
    }

    /// Shows ready UI when player reaches entrance.
    /// <param name="other">Object entering the entrance area.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.ShowTakeOffReady();
        }
    }

    /// Hides ready UI when player leaves entrance.
    /// <param name="other">Object exiting the entrance area.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.HideTakeOffReady();
        }
    }
}