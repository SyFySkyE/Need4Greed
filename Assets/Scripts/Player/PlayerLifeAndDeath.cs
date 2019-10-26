using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static PlayerManage;

public class PlayerLifeAndDeath : MonoBehaviour
{
    [SerializeField] private int healthPoints = 3;    
    [SerializeField] private float secondsBeforeLoad = 3f;
    [SerializeField] private TextMeshProUGUI hpText;

    private PlayerManage playerManager;
    private bool vulnerable = true;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.layer = 8;
        playerManager = GetComponent<PlayerManage>();
        UpdateHPText();
    }

    private void UpdateHPText()
    {
        hpText.text = healthPoints.ToString();
    }

    // Update is called once per frame
    void Update()
    {        
        if (healthPoints <= 0)
        {
            CommencePlayerDying();
        }        
    }

    public void CommencePlayerDying()
    {
        this.gameObject.layer = 9;
        if (playerManager.State != PlayerState.Dead)
        {
            StartCoroutine(GameOverRoutine());
            playerManager.State = PlayerState.Dying;
        }        
    }

    private IEnumerator GameOverRoutine()
    {
        vulnerable = false;
        yield return new WaitForSeconds(secondsBeforeLoad);        
    }

    private void PlayerHurt()
    {        
        if (vulnerable)
        {
            healthPoints--;
            UpdateHPText();
            vulnerable = false;
            StartCoroutine(Recover());
        }        
    }

    private IEnumerator Recover()
    {        
        yield return new WaitForSeconds(playerManager.RecoveryTime);        
        vulnerable = true;
    }
}
