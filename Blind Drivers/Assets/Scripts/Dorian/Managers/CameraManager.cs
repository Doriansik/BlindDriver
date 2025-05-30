using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private Camera playerCamera;
    [SerializeField] private Camera carCamera;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V)) SwitchToCarCamera();
        if(Input.GetKeyDown(KeyCode.B)) SwitchToPlayerCamera();
    }

    public void SwitchToPlayerCamera()
    {
        playerCamera.enabled = true;
        carCamera.enabled = false;
    }

    public void SwitchToCarCamera()
    {
        playerCamera.enabled = false;
        carCamera.enabled = true;
    }
}
