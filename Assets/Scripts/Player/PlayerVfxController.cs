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
        SkinnedMeshRenderer renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        for (float i = 1; i <= 0;)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds(i);
            i -= 0.1f;
        }
    }
}
