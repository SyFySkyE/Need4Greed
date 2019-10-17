using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int coinsCollected = 0;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private int coinBonus = 10;
    [SerializeField] private float jumpBonus = 10f;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinText();
    }

    private void Update()
    {
        
    }

    private void UpdateCoinText()
    {
         coinText.text = coinsCollected.ToString();              
    }

    public void AddToScore()
    {
        coinsCollected++;
        UpdateCoinText();
    }

    public void JumpedOnEnemy()
    {
        coinsCollected += coinBonus;
        GetComponent<Rigidbody>().AddForce(0f, jumpBonus, 0f, ForceMode.Impulse);
        UpdateCoinText();
    }
}
