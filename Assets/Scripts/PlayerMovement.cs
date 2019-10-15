using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 2f;
    [SerializeField] private float jumpForce = 17f;
    [SerializeField] private float xConstrain = 5.5f;
    [SerializeField] private float gravityModifier = 3f;
    [SerializeField] private float secondsBeforeReload = 1.5f;
    [SerializeField] private float jumpVolume = 5f;

    [SerializeField] private AudioClip jumpSfx;
    [SerializeField] private ParticleSystem dustKickVfx;

    [SerializeField] private SceneManager sceneManager; // TODO PlayerMovement should NOT know about sceneManager. Maybe use broadcastMessage()? Soc

    private Rigidbody playerRB;
    private Animator playerAnim;
    private AudioSource playerAudio;

    private bool canJump = true; // TODO Switch to enum
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier; // TODO Bug where jumping is borked when scene is reloaded. Could be this.
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameOver);
        if (!gameOver) MoveForward();
        else
        {
            StartCoroutine(StartGameOverSequence());
        }
        MoveHorizontally();
        Jump();
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            dustKickVfx.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
        }
    }

    private IEnumerator StartGameOverSequence() 
    {
        dustKickVfx.Stop();
        yield return new WaitForSeconds(secondsBeforeReload);
        sceneManager.ReloadScene(); // TODO SoC
    }
}
