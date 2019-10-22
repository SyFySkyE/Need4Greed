using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManage;

public class PlayerSfxController : MonoBehaviour
{
    [Header("SFX Audioclips")]
    [SerializeField] private AudioClip jumpSfx;
    [SerializeField] private AudioClip landSfx;
    [SerializeField] private AudioClip deathSfx;
    [SerializeField] private AudioClip[] coinSfx;

    [Header("SFX Volume")]
    [SerializeField] private float jumpSfxVolume = 5f;
    [SerializeField] private float landSfxVolume = 1f;
    [SerializeField] private float deathSfxVolume = 5f;
    [SerializeField] private float coinSfxVolume = 1f;

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
                playerAudio.PlayOneShot(deathSfx, deathSfxVolume);
                break;
        }
    }
}
