using UnityEngine;

public interface IPickable
{
    void PickUpItem(Transform parent);
    void DropItem(Vector3 forceDirection);
}
