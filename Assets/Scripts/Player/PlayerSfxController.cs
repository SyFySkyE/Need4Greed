using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManage;

public class PlayerSfxController : MonoBehaviour
{
    [Header("SFX Audioclips")]
    [SerializeField] private AudioClip jumpSfx;
    [SerializeField] private AudioClip landSfx;
    [SerializeField] private AudioClip landOnEnemySfx;
    [SerializeField] private AudioClip deathSfx;    

    [Header("SFX Volume")]
    [SerializeField] private float jumpSfxVolume = 5f;
    [SerializeField] private float landSfxVolume = 1f;
    [SerializeField] private float landOnEnemySfxVolume = 1f;
    [SerializeField] private float deathSfxVolume = 5f;

    private AudioSource playerAudio;
    private PlayerManage playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerManager = GetComponent<PlayerManage>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleState();
    }

    private void HandleState()
    {
        switch (playerManager.State)
        {
            case PlayerState.Jumping:
                playerAudio.PlayOneShot(jumpSfx, jumpSfxVolume);
                break;
            case PlayerState.Dying:
            case PlayerState.Hurt:
                playerAudio.PlayOneShot(deathSfx, deathSfxVolume);
                break;
            case PlayerState.LandingOnEnemy:
                playerAudio.PlayOneShot(landOnEnemySfx, landOnEnemySfxVolume);
                jumpSfxVolume = 0f;
                break;
        }
    }
}
