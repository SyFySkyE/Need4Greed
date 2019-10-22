using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManage : MonoBehaviour
{
    public enum PlayerState { Running, Jumping, Jumped, Landing, LandingOnEnemy, Stopping, Stopped, Dying, Dead }
    private PlayerState state;
    public PlayerState State
    {
        get { return state; }
        set
        {
            if (state != value)
            {
                state = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.Running;
    }

    private void Update()
    {
        Debug.Log("Player state: " + State);
        HandleChangeStates();
    }

    private void HandleChangeStates()
    {
        switch (State)
        {
            case PlayerState.Landing:
                State = PlayerState.Running;
                break;
            case PlayerState.LandingOnEnemy:
                State = PlayerState.Jumping;
                break;
            case PlayerState.Jumping:
                State = PlayerState.Jumped;
                break;
            case PlayerState.Dying:
                State = PlayerState.Dead;
                break;
        }
    }
}
