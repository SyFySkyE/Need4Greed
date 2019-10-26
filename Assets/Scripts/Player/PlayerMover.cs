using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManage;

public class PlayerMover : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float forwardSpeed = 15f;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float xConstraint = 4.9f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float bonusJumpForce = 20f;

    [Header("Movement Increments")]
    [SerializeField] private float forwardSpeedIncrement = 10f;
    [SerializeField] private float horizontalSpeedIncrement = 2.5f;

    private PlayerManage playerManager;
    private Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManage>();
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager.State != PlayerState.Dead)
        {
            MoveForward();
            MoveHorizontally();
            ConstrainHorizontalMovement();
            Jump();
        }
        else 
        {
            Stop();
        }
        switch (playerManager.CurrentLevelState)
        {
            case LevelState.OneTwo:
            case LevelState.TwoThree:
                IncreaseSpeed();
                break;
        }
    }

    private void Stop()
    {
        playerRB.velocity = Vector3.zero;
    }

    private void MoveForward()
    {
        playerRB.velocity = new Vector3(playerRB.velocity.x, playerRB.velocity.y, forwardSpeed);
    }

    private void MoveHorizontally()
    {
        float xAxisRaw = Input.GetAxis("Horizontal");
        playerRB.velocity = new Vector3(xAxisRaw * horizontalSpeed, playerRB.velocity.y, playerRB.velocity.z);
    }

    private void ConstrainHorizontalMovement()
    {
        if (transform.position.x >= xConstraint)
        {
            transform.position = new Vector3(xConstraint, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -xConstraint)
        {
            transform.position = new Vector3(-xConstraint, transform.position.y, transform.position.z);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && playerManager.State == PlayerState.Running)
        {
            playerManager.State = PlayerState.Jumping;
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);            
        }        
    }   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && playerManager.State == PlayerState.Jumped)
        {
            playerManager.State = PlayerState.Landing;
        }
        else if (collision.gameObject.CompareTag("Checkpoint"))
        {            
            forwardSpeed += forwardSpeedIncrement;
            horizontalSpeed += horizontalSpeedIncrement;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            LandOnEnemy(collision.gameObject);
        }
    }

    private void LandOnEnemy(GameObject enemy)
    {
        if (playerManager.State != PlayerState.Recovering)
        {
            enemy.GetComponent<Enemy>().EnemyDeath();
            playerManager.State = PlayerState.LandingOnEnemy;
            playerRB.AddForce(Vector3.up * bonusJumpForce, ForceMode.Impulse);
            enemy.layer = 9; // Players do not collide with anything in this layer.
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Enemy"))
        {              
            if (playerManager.State != PlayerState.Hurt && playerManager.State != PlayerState.Recovering)
            {
                playerManager.State = PlayerState.Hurt;
                other.gameObject.layer = 9; // Players will not collide with anything in this layer.
            }            
        }
    }

    private void PlayerHurt()
    {
        playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
    }

    private void IncreaseSpeed()
    {
        forwardSpeed += forwardSpeedIncrement;
        horizontalSpeed += horizontalSpeedIncrement;
    }
}
