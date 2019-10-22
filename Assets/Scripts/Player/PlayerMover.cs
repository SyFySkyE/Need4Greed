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
        if (collision.gameObject.CompareTag("Ground"))
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
        enemy.GetComponent<Enemy>().EnemyDeath();
        playerManager.State = PlayerState.LandingOnEnemy;
        playerRB.AddForce(Vector3.up * bonusJumpForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Enemy"))
        {
            playerManager.State = PlayerState.Hurt;
        }
    }
}
