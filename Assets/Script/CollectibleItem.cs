/*
 * Description: Allows player to collect repair items using E and plays a collection sound.
 */

using UnityEngine;

/// Collectible item that can be picked up by the player.
public class CollectibleItem : InteractableObject
{
    /// Type of collectible this object gives.
    [SerializeField] private CollectibleType collectibleType;

    /// Sound played when the item is collected.
    [SerializeField] private AudioClip collectSound;

    /// Volume of the collection sound.
    [SerializeField] private float collectVolume = 1f;

    /// Gives the item to GameManager, plays sound, and removes the object.
    public override void Interact()
    {
        if (!canInteract)
        {
            return;
        }

        canInteract = false;

        GameManager.instance.CollectItem(collectibleType);
        GameManager.instance.ClearPrompt();

        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position, collectVolume);
        }

        Destroy(gameObject);
    }
}