using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerScoreManager : NetworkBehaviour
{
    [SerializeField] private int gateAmount;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Canvas playerCanvas;

    NetworkVariable<int> score = new NetworkVariable<int>();


    private void Update()
    {
        if(!IsOwner)
        {
            playerCanvas.gameObject.SetActive(false);
        }

        if (IsOwner)
        {
            scoreText.text = "Score: " + score.Value.ToString();
        }
    }

    public void AddGateAmount()
    {
        gateAmount++;

        if (gateAmount == 2)
        {
            gateAmount = 0;
            AddScore(1);
        }
    }

    public void AddScore(int amount)
    {
        if (IsServer)
        {
            score.Value += amount;
        }
    }
}
