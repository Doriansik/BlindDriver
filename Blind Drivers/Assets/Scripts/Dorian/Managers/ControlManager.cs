using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public static ControlManager Instance { get; private set; }

    [SerializeField] private PlayerMovement playerMovement;
    private IControllable currentControllable;

    private void Awake()
    {
        Instance = this;
        SetControl(playerMovement);
    }


    public void SetControl(IControllable newControllable)
    {
        currentControllable?.DisableControl();
        currentControllable = newControllable;
        currentControllable?.EnableControl();
    }

}
