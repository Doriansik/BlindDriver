using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        transform.position = player.position;
    }
}
