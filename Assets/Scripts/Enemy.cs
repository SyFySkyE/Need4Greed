using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float xConstraint = 4.5f;

    private Animator enemyAnim;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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
    }

    private void OnTriggerEnter(Collider other) // Box is only Trigger, should trigger death.
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerManager>().JumpedOnEnemy();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().ToggleGameOver();
        }
    }
}
