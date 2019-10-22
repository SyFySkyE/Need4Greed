using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManage;

public class PlayerAnimator : MonoBehaviour
{
    private Animator playerAnim;

    private PlayerManage playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManage>();
    }    

    // Update is called once per frame
    void Update()
    {
        if (playerManager.State == PlayerState.Running)
        {
            SetMovingAnim();            
        }
        else if (playerManager.State == PlayerState.Jumped)
        {
            SetJumpingAnim();
        }
        else if (playerManager.State == PlayerState.Landing)
        {
            SetLandingAnim();
        }
        else if (playerManager.State == PlayerState.LandingOnEnemy)
        {
            SetLandingOnEnemyAnim();
        }
        else if (playerManager.State == PlayerState.Dying)
        {
            SetDyingAnim();
        }
    }

    private void SetMovingAnim()
    {
        playerAnim.SetFloat("Speed_f", 0.7f);
    }

    private void SetJumpingAnim()
    {
        playerAnim.SetBool("Jump_b", true);        
    }

    private void SetLandingAnim()
    {
        playerAnim.SetBool("Jump_b", false);
    }

    private void SetLandingOnEnemyAnim()
    {
        playerAnim.SetBool("JumpOn_b", true);
    }

    private void SetDyingAnim()
    {
        playerAnim.SetTrigger("Death_t");
    }
}
