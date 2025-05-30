using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float throwForce;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private Transform orientation;
    private IPickable heldItem;

    private void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IInteractable interactable = GetInteractableObject();
            if (interactable != null)
            {
                if (heldItem == null && interactable is IPickable pickable)
                {
                    heldItem = pickable;
                    heldItem.PickUpItem(holdPoint);
                }
                else
                {
                    interactable.Interact(transform);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && heldItem != null)
        {
            Vector3 throwDirection = orientation.forward * throwForce;
            heldItem.DropItem(throwDirection);
            heldItem = null;
        }

    }

    public IInteractable GetInteractableObject()
    {
        List<IInteractable> interactableList = new List<IInteractable>();
        float interactRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out IInteractable interactable)) 
            {
                interactableList.Add(interactable);
            }
        }

        IInteractable closestInteractable = null;
        foreach (IInteractable interactable in interactableList) 
        {
            if (closestInteractable == null)
            {
                closestInteractable = interactable;
            }
            else
            {
                if(Vector3.Distance(transform.position, interactable.GetTransform().position) < Vector3.Distance(transform.position, closestInteractable.GetTransform().position))
                {
                    closestInteractable = interactable;
                }
            }
        }

        return closestInteractable;
    }
}
