using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static PlayerManage;

public class PlayerCoinCollector : MonoBehaviour
{
    [SerializeField] private int coinsCollected = 0;
    [SerializeField] private int coinBonus = 10;
    [SerializeField] private TextMeshProUGUI coinText;

    private PlayerManage playerManager;
    private Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerManager = GetComponent<PlayerManage>();
        UpdateCoinText();
    }

    private void Update()
    {
        IfLandsOnEnemy();
    }

    private void IfLandsOnEnemy()
    {
        if (playerManager.State == PlayerState.LandingOnEnemy)
        {
            AddCoinBonus();
        }
    }

    private void UpdateCoinText()
    {
        coinText.text = coinsCollected.ToString();
    }

    public int GetCoinsCollected()
    {
        return coinsCollected;
    }

    public void AddCoin()
    {
        coinsCollected++;
        UpdateCoinText();
    }

    private void AddCoinBonus()
    {
        coinsCollected += coinBonus;
        UpdateCoinText();
    }
}
