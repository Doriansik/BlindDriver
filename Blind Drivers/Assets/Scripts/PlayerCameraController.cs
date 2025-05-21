using Unity.Netcode;
using UnityEngine;

public class PlayerCameraController : NetworkBehaviour
{
    [SerializeField] private Camera playerCamera;

    private void Start()
    {
        if (!IsOwner && playerCamera != null)
        {
            playerCamera.enabled = false;
            return;
        }

        if (IsOwner && playerCamera != null)
        {
            playerCamera.enabled = true;
        }
    }
}
