using System.Collections;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RelayManager : NetworkBehaviour
{
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button joinBtn;
    [SerializeField] private TMP_InputField joinInput;
    [SerializeField] private TextMeshProUGUI codeText;

    async void Start()
    {
        await UnityServices.InitializeAsync();

        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        hostBtn.onClick.AddListener(CreateRelay);
        joinBtn.onClick.AddListener(() => JoinRelay(joinInput.text));
    }

    async void CreateRelay()
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);
        string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        codeText.text = "Code: " + joinCode;


        var relayServerData = AllocationUtils.ToRelayServerData(allocation, "dtls");

        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

        NetworkManager.Singleton.StartHost();

        StartCoroutine(WaitToChangeScene());

    }

    async void JoinRelay(string joinCode)
    {
        var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

        var relayServerData = AllocationUtils.ToRelayServerData(joinAllocation, "dtls");

        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

        NetworkManager.Singleton.StartClient();
        NetworkManager.SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }

    private IEnumerator WaitToChangeScene()
    {
        yield return new WaitForSeconds(5f);
        NetworkManager.SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }
}
