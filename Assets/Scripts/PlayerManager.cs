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
    [SerializeField] private ParticleSystem landOnEnemyVfx;
    [SerializeField] private AudioClip landOnEnemySfx;

    private PlayerMovement pMovement;

    // Start is called before the first frame update
    void Start()
    {
        pMovement = GetComponent<PlayerMovement>();
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
        landOnEnemyVfx.Play();
        GetComponent<Rigidbody>().AddForce(0f, jumpBonus, 0f, ForceMode.Impulse);
        GetComponent<AudioSource>().PlayOneShot(landOnEnemySfx, 1f);
        UpdateCoinText();
    }

    public int GetCoinsCollected()
    {
        return coinsCollected;
    }

    public void GoalPassed()
    {        
        Debug.Log("Player passed");
        pMovement.IncreaseSpeed();
    }
    
    public void GameOver()
    {
        pMovement.GoalFailed();
    }
}
