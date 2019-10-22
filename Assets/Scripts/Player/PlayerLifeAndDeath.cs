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

    public void SubtractHP()
    {
        healthPoints--;
        UpdateHPText();
    }
}
