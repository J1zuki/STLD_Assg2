/*
 * Description: Allows player to collect repair items using E.
 */

using UnityEngine;

/// Collectible item that can be picked up by the player.
public class CollectibleItem : InteractableObject
{
    /// Type of collectible this object gives.
    [SerializeField] private CollectibleType collectibleType;

    /// Gives the item to GameManager and removes the object.
    public override void Interact()
    {
        if (!canInteract)
        {
            return;
        }

        canInteract = false;
        GameManager.instance.CollectItem(collectibleType);
        GameManager.instance.ClearPrompt();
        Destroy(gameObject);
    }
}