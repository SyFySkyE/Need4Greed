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
    [SerializeField] private const int timeWhileFlashing = 5;

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
        if (currentState == PlayerState.Running)
        {
            StartMoveVfx();
        }
        else if (currentState == PlayerState.Jumped)
        {
            StartJumpVfx();
        }
        else if (currentState == PlayerState.Landing)
        {            
            landVfx.Play(); // Plays once
        }
        else if (currentState == PlayerState.LandingOnEnemy)
        {
            PlayLandingOnEnemyVfx();
        }
        else if (currentState == PlayerState.Dying)
        {
            dustKickVfx.Stop();
            failVfx.Play(); // Plays once
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
    }

    private IEnumerator FlashPlayer()
    {
        // This works, but this may not sync up with 1.5f recovery time set in PlayerManage. Set up a timer to make sure it lines up.
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
