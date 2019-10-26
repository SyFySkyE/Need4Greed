using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManage;

public class PlayerVfxController : MonoBehaviour
{
    [Header("Particle System Prefabs")]
    [SerializeField] ParticleSystem dustKickVfx;
    [SerializeField] ParticleSystem jumpingVfx;
    [SerializeField] ParticleSystem landVfx;
    [SerializeField] ParticleSystem landOnEnemyVfx;
    [SerializeField] ParticleSystem hurtVfx;
    [SerializeField] ParticleSystem failVfx;

    [SerializeField] private float playerFlashInterval = 0.01f;
    [SerializeField] private int timeWhileFlashing = 15;

    private PlayerManage playerManager;

    private void Start()
    {
        playerManager = GetComponent<PlayerManage>();
    }

    // Update is called once per frame
    void Update()
    {
        ManagePlayerState(playerManager.State);        
    }

    private void ManagePlayerState(PlayerState currentState)
    {
        if (currentState != PlayerState.Dead || currentState != PlayerState.Dying)
        {
            switch (currentState)
            {
                case PlayerState.Running:
                    StartMoveVfx();
                    break;
                case PlayerState.Jumped:
                    StartJumpVfx();
                    break;
                case PlayerState.Landing:
                    landVfx.Play();
                    break;
                case PlayerState.LandingOnEnemy:
                    PlayLandingOnEnemyVfx();
                    break;
                case PlayerState.Dying:
                    PlayDeathVfx();
                    break;
            }
        }
    }

    private void StartMoveVfx()
    {
        if (!dustKickVfx.isPlaying)
        {
            dustKickVfx.Play();
            jumpingVfx.Stop();
        }        
    }

    private void StartJumpVfx()
    {
        if (!jumpingVfx.isPlaying)
        {
            dustKickVfx.Stop();
            jumpingVfx.Play();
        }        
    }

    private void PlayLandingOnEnemyVfx()
    {
        landOnEnemyVfx.Play();        
    }

    private void PlayerHurt()
    {
        StartCoroutine(FlashPlayer());
        hurtVfx.Play();
                      
    }

    private void PlayDeathVfx()
    {
        dustKickVfx.Stop();
        failVfx.Play();         
    }

    private IEnumerator FlashPlayer()
    {        
        SkinnedMeshRenderer render = GetComponentInChildren<SkinnedMeshRenderer>();
        for (int i = 1; i < timeWhileFlashing; i++)
        {
            
            render.enabled = false;
            yield return new WaitForSeconds(playerFlashInterval);
            render.enabled = true;
            yield return new WaitForSeconds(playerFlashInterval);            
        }
    }
}
