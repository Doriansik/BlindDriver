using Unity.Netcode;
using UnityEngine;
using TMPro;

public class PlayerScoreManager : NetworkBehaviour
{
    [SerializeField] private Canvas playerCanvas;
    [SerializeField] private TextMeshProUGUI scoreText;

    private NetworkVariable<int> score = new NetworkVariable<int>();
    private int gateAmount = 0;

    private void Start()
    {
        if (!IsOwner)
        {
            playerCanvas.gameObject.SetActive(false);
        }
        else
        {
            playerCanvas.gameObject.SetActive(true);
            scoreText.text = "Score: " + score.Value;
        }

        score.OnValueChanged += (oldValue, newValue) =>
        {
            if (IsOwner)
            {
                scoreText.text = "Score: " + newValue;
            }
        };
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

    private void AddScore(int amount)
    {
        if (IsServer)
        {
            score.Value += amount;
        }
    }
}
