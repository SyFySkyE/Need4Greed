using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManage : MonoBehaviour
{
    [SerializeField] public float RecoveryTime = 1.5f;
    public enum PlayerState { Running, Jumping, Hurt, Recovering, Jumped, Landing, LandingOnEnemy, Stopping, Stopped, Dying, Dead }
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

    private void HandleChangeStates() // Switched to Coroutines, was sometimes transitioning too fast, espeically in environments producing lower frame rates
    {
        switch (State)
        {
            case PlayerState.Landing:
                StartCoroutine(SwitchToRunningState());
                break;
            case PlayerState.LandingOnEnemy:
                StartCoroutine(SwitchToJumpingState());
                break;
            case PlayerState.Jumping:
                StartCoroutine(SwitchToJumpedState());
                break;
            case PlayerState.Hurt:                        
                StartCoroutine(SwitchToRecoveringState());
                break;
            case PlayerState.Recovering:
                break;
            case PlayerState.Dying:
                StartCoroutine(SwitchToDeadState());                
                break;
        }
    }    

    private IEnumerator SwitchToRunningState()
    {
        yield return new WaitForEndOfFrame();
        State = PlayerState.Running;
    }

    private IEnumerator SwitchToJumpingState()
    {
        yield return new WaitForEndOfFrame();
        State = PlayerState.Jumping;
    }

    private IEnumerator SwitchToJumpedState()
    {
        yield return new WaitForEndOfFrame();
        State = PlayerState.Jumped;
    }
    
    private IEnumerator SwitchToRecoveringState()
    {        
        State = PlayerState.Recovering;        
        BroadcastMessage("PlayerHurt");        
        yield return new WaitForSeconds(RecoveryTime);
        
        StartCoroutine(SwitchToRunningState());
    }

    private IEnumerator SwitchToDeadState()
    {
        yield return new WaitForEndOfFrame();
        State = PlayerState.Dead;
    }
}
