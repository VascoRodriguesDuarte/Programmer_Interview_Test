using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCheck : MonoBehaviour
{
    [SerializeField] private Interact currentObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter");
        // Check if the other object has the Interact component
        Interact interactObject = other.GetComponent<Interact>();
        if (interactObject != null)
        {
            currentObject = interactObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exit");
        if (currentObject == other.GetComponent<Interact>())
        {
            currentObject = null;
        }
    }

    public void ActivateInteract()
    {
        if(currentObject != null)
        {
            Debug.Log("Done");
            currentObject.PublicInteract();
        }
    }
}
