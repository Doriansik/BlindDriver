using TMPro;
using Unity.Netcode;
using UnityEngine;

public class Gate : NetworkBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (IsServer)
            {
                PlayerScoreManager playerScoreManager = other.GetComponent<PlayerScoreManager>();

                if (playerScoreManager != null)
                {
                    playerScoreManager.AddGateAmount();
                }
            }
        }
    }
}
