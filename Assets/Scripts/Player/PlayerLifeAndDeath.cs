using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static PlayerManage;

public class PlayerLifeAndDeath : MonoBehaviour
{
    [SerializeField] private int healthPoints = 3;
    [SerializeField] private float recoveryTime = 1f;
    [SerializeField] private float secondsBeforeLoad = 3f;
    [SerializeField] private TextMeshProUGUI hpText;

    private PlayerManage playerManager;
    private bool vulnerable = true;

    // Start is called before the first frame update
    void Start()
    {
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

    private void CommencePlayerDying()
    {
        if (playerManager.State == PlayerState.Dying)
        {
            StartCoroutine(GameOverRoutine());
        }
    }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(secondsBeforeLoad);
        Debug.Log("Respawn at checkpoint");
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
        SkinnedMeshRenderer renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        for (float i = recoveryTime; i != 0;)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds(i);
            renderer.enabled = true; // Nogo
            i -= 0.1f;
        }
        vulnerable = true;
    }
}
