using UnityEngine;

public class Map : MonoBehaviour, IInteractable, IPickable
{
    private Rigidbody rb;
    private Collider col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void PickUpItem(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(35f, 0f, 0f);

        if (rb != null) rb.isKinematic = true;
        if (col != null) col.enabled = false;
    }

    public void DropItem(Vector3 force)
    {
        transform.SetParent(null);

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(force, ForceMode.Impulse);
        }
        col.enabled = true;
    }

    public void Interact(Transform interactorTransform)
    {
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
