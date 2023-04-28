using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCheck : MonoBehaviour
{
    [SerializeField] private Interact currentObject;
    [SerializeField] private PlayerMovement player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Interact interactObject = other.GetComponent<Interact>();
        if (interactObject != null)
        {
            currentObject = interactObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (currentObject == other.GetComponent<Interact>())
        {
            currentObject = null;
        }
    }

    public void ActivateInteract()
    {
        if(currentObject != null)
        {
            currentObject.PublicInteract();
            player.isInteracting = true;
        }
    }
}
