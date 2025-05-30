using UnityEngine;

public class Car : MonoBehaviour, IInteractable, IControllable
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform driverSeat;
    [SerializeField] private Transform exitPoint;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private CarMovement carMovement;

    private bool isPlayerInside = false;

    public void Interact(Transform interactorTransform)
    {
        if (!isPlayerInside)
        {
            EnterCar();
        }
        else
        {
            ExitCar();
        }
    }

    private void EnterCar()
    {
        player.SetActive(false);
        ControlManager.Instance.SetControl(carMovement);
        CameraManager.Instance.SwitchToCarCamera();
        player.transform.SetPositionAndRotation(driverSeat.position, driverSeat.rotation);
        isPlayerInside = true;
        player.SetActive(true);
    }

    private void ExitCar()
    {
        ControlManager.Instance.SetControl(playerMovement);
        CameraManager.Instance.SwitchToPlayerCamera();
        player.transform.SetPositionAndRotation(exitPoint.position, exitPoint.rotation);
        isPlayerInside = false;
        carMovement.StopCar();
    }

    public Transform GetTransform()
    {
       return transform;
    }

    public void EnableControl()
    {
        enabled = true;
    }

    public void DisableControl()
    {
        enabled = false;
    }
}
