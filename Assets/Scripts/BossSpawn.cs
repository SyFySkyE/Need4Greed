using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float fasterMoveSpeed = 40f;
    [SerializeField] private EnemyBoss dragon;

    private PlayerManage player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerManage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dragon == null)
        {
            float moveStep = moveSpeed * Time.deltaTime;
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
            //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveStep);
        }
        else
        {
            moveSpeed = fasterMoveSpeed;
            float moveStep = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, dragon.transform.position, moveStep);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerManage>().State = PlayerManage.PlayerState.Hurt;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dragon = FindObjectOfType<EnemyBoss>();
            other.GetComponent<PlayerMover>().LandOnBossEnemy();
            this.gameObject.layer = 9;
            dragon.StateTwoThreeTransition();
        }
    }
}
