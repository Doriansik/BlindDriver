using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Gate : NetworkBehaviour
{
    [SerializeField] private int gateAmount;
    [SerializeField] private TextMeshProUGUI scoreTextTMP;

    private NetworkVariable<int> scoreOnline = new NetworkVariable<int>(0, NetworkVariableReadPermission.Owner,NetworkVariableWritePermission.Owner);

    private int score;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gateAmount++;

            if(gateAmount == 2)
            {
                score++;
                scoreOnline.Value++;
                gateAmount = 0;
                scoreTextTMP.text = "Score: " + score.ToString();
            }
        }
    }
}
