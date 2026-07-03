/*
 * Description: Allows player to press E near interactable objects.
 */

using UnityEngine;

/// Detects nearby interactable objects and lets the player interact using E.
public class PlayerInteract : MonoBehaviour
{
    /// Current interactable object near the player.
    private InteractableObject currentInteractable;

    /// Checks for E input.
    private void Update()
    {
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact();

            if (!currentInteractable.IsInteractable())
            {
                currentInteractable = null;
                GameManager.instance.ClearPrompt();
            }
        }
    }

    /// Detects interactable object when player enters trigger.
    /// <param name="other">Object entered.</param>
    private void OnTriggerEnter(Collider other)
    {
        InteractableObject interactable = other.GetComponent<InteractableObject>();

        if (interactable != null && interactable.IsInteractable())
        {
            currentInteractable = interactable;
            GameManager.instance.ShowPrompt(interactable.GetPromptMessage());
        }
    }

    /// Clears interactable object when player exits trigger.
    /// <param name="other">Object exited.</param>
    private void OnTriggerExit(Collider other)
    {
        InteractableObject interactable = other.GetComponent<InteractableObject>();

        if (interactable != null && interactable == currentInteractable)
        {
            currentInteractable = null;
            GameManager.instance.ClearPrompt();
        }
    }
}