using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float swaySpeed = 2f;
    [SerializeField] private float xConstraint = 4.5f;
    [SerializeField] private bool xSway = false;
    [SerializeField] private float xSwayValue = 2f;
    [SerializeField] private bool ySway = false;
    [SerializeField] private float ySwayValue = -1f;
    [SerializeField] private float distanceToStartMoving = 10f;
    [SerializeField] private GameObject player;
    [SerializeField] private float secondsBeforeDespawn = 1f;

    private Animator enemyAnim;
    private bool moveRight = true;
    private bool moveUp = false;
    private bool isCloseEnough = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        IsPlayerCloseEnough();
        if (isCloseEnough)
        {
            StartMoving();
        }        
    }

    private void IsPlayerCloseEnough()
    {
        if (player.transform.position.z >= transform.position.z - distanceToStartMoving)
        {
            isCloseEnough = true;
        }
    }

    private void StartMoving()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (transform.position.x >= xConstraint)
        {
            speed = -speed;
            // enemyAnim.SetTrigger("Rotate Right"); TODO Anims.
        }
        else if (transform.position.x <= -xConstraint)
        {
            speed = -speed;
            // enemyAnim.SetTrigger("Rotate Left");
        }

        if (xSway)
        {
            SwayHorizontally();
        }

        if (ySway)
        {
            SwayVertically();
        }
    }

    private void SwayHorizontally()
    {        
        if (moveRight)
        {
            transform.Translate(Vector3.right * Time.deltaTime * swaySpeed);
            if (transform.position.x >= xSwayValue) moveRight = false;
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * swaySpeed);
            if (transform.position.x <= -xSwayValue) moveRight = true;
        }
    }

    private void SwayVertically()
    {
        if (moveUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * swaySpeed);
            if (transform.position.y >= -ySwayValue) moveUp = false;
        }
        else
        {
            transform.Translate(Vector3.down * Time.deltaTime * swaySpeed);
            if (transform.position.y <= 0.5f) moveUp = true;
        }
    }

    public void EnemyDeath()
    {
        enemyAnim.SetTrigger("Stomped");
        Destroy(gameObject, secondsBeforeDespawn);
    }
}
