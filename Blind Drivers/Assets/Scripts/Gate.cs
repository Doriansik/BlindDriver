using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Gate : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && IsServer)
        {
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
