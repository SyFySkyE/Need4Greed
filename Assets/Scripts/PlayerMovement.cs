using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 2f;
    [SerializeField] private float forwardIncrementSpeed = 10f;
    [SerializeField] private float horizontalIncrementSpeed = 2.5f;
    [SerializeField] private float jumpForce = 17f;
    [SerializeField] private float xConstrain = 5.5f;
    [SerializeField] private float secondsBeforeReload = 2f;
    [SerializeField] private float jumpVolume = 5f;

    [SerializeField] private AudioClip jumpSfx;
    [SerializeField] private AudioClip deathSfx;
    [SerializeField] private ParticleSystem dustKickVfx;
    [SerializeField] private ParticleSystem jumpingVfx;
    [SerializeField] private ParticleSystem landVfx;
    [SerializeField] private ParticleSystem failVfx;

    [SerializeField] private GameSceneManager sceneManager; // TODO PlayerMovement should NOT know about sceneManager. Maybe use broadcastMessage()? Soc

    private Rigidbody playerRB;
    private Animator playerAnim;
    private AudioSource playerAudio;

    private bool canJump = true; // TODO Switch to enum
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        DebugKeys();
        Debug.Log(gameOver);
        if (!gameOver) MoveForward();
        MoveHorizontally();
        Jump();
    }

    private void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.position = new Vector3(0f, 0f, 560f);
            forwardSpeed = 30f;
            horizontalSpeed = 14.5f;
        }
    }

    private void MoveForward()
    {
        playerRB.velocity = new Vector3(playerRB.velocity.x, playerRB.velocity.y, forwardSpeed);
        playerAnim.SetFloat("Speed_f", 0.7f);
    }

    private void MoveHorizontally()
    {
        if (transform.position.x >= xConstrain)
        {
            transform.position = new Vector3(xConstrain, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -xConstrain)
        {
            transform.position = new Vector3(-xConstrain, transform.position.y, transform.position.z);
        }
        float xRaw = Input.GetAxis("Horizontal");
        playerRB.velocity = new Vector3(xRaw * horizontalSpeed, playerRB.velocity.y, playerRB.velocity.z);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && canJump)
        {
            playerAudio.PlayOneShot(jumpSfx, jumpVolume);
            playerAnim.SetTrigger("Jump_trig");            
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
            dustKickVfx.Stop();            
        }

        if (!canJump && playerRB.velocity.y != 0)
        {
            jumpingVfx.Play();
        }
        else
        {
            jumpingVfx.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (canJump != true)
            {
                landVfx.Play();
            }
            canJump = true;
            dustKickVfx.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {            
            gameOver = true;
            GoalFailed();
        }
    }

    private IEnumerator StartGameOverSequence()  // This should be in PlayerManager
    {
        dustKickVfx.Stop();
        StartCoroutine(ToggleSpeedOff());
        failVfx.Play();
        playerAudio.PlayOneShot(deathSfx, 5f);
        yield return new WaitForSeconds(secondsBeforeReload);
        sceneManager.ReloadScene(); // TODO SoC
    }

    public void ToggleGameOver()
    {
        gameOver = true;        
    }

    public void IncreaseSpeed()
    {
        forwardSpeed += forwardIncrementSpeed;
        horizontalSpeed += horizontalIncrementSpeed;
    }

    public void GoalFailed()
    {        
        gameOver = true;
        StartCoroutine(StartGameOverSequence());
    }

    private IEnumerator ToggleSpeedOff()
    {
        float currentFSpeed = forwardSpeed;
        float currentHSpeed = horizontalSpeed;
        forwardSpeed = 0f;
        horizontalSpeed = 0f;
        yield return new WaitForSeconds(secondsBeforeReload);
        forwardSpeed = currentFSpeed;
        horizontalSpeed = currentHSpeed;
    }
}
