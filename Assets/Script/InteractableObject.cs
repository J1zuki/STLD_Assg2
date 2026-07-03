/*
 * Description: Base class for objects that can be interacted with.
 */

using UnityEngine;

/// Parent class for interactable objects.
public class InteractableObject : MonoBehaviour
{
    /// Name shown in interaction prompt.
    [SerializeField] protected string interactName = "Object";

    /// Checks if the object can currently be interacted with.
    protected bool canInteract = true;

    /// Gets the prompt message.
    /// <returns>Prompt message.</returns>
    public virtual string GetPromptMessage()
    {
        return "Press E to interact with " + interactName;
    }

    /// Runs when player interacts with object.
    public virtual void Interact()
    {
        Debug.Log("Interacted with " + interactName);
    }

    /// Checks if object is still interactable.
    /// <returns>True if object can be interacted with.</returns>
    public bool IsInteractable()
    {
        return canInteract;
    }
}