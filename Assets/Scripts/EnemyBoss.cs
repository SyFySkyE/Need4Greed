using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float yPos = 10;
    [SerializeField] private float zPos = 25f;
    [SerializeField] private float zDistanceToSpawn = 50f;
    [SerializeField] private float xConstraint = 5f;
    [SerializeField] private float moveSpeed = 3f;

    private enum BossState { Despawned, Spawning, Spawned, Moving, Firing, Dead, PhaseOne, PhaseTwo, PhaseThree}
    private BossState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = BossState.Despawned;
    }

    // Update is called once per frame
    void Update()
    {
        HandleStates();
        Spawn();
    }

    private void HandleStates()
    {
        switch (currentState)
        {
            case BossState.Despawned:
                Spawn();
                break;
            case BossState.Spawning:
                currentState = BossState.Moving;
                break;
            case BossState.Moving:
                Move();
                break;
        }
    }

    private void Spawn()
    {        
        if (transform.position.z - player.transform.position.z <= zDistanceToSpawn)
        {
            currentState = BossState.Moving;
        }
    }

    private void Move()
    {        
        transform.position = new Vector3(transform.position.x, yPos, zPos + player.transform.position.z);
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        if (transform.position.x >= xConstraint || transform.position.x <= -xConstraint)
        {
            moveSpeed = -moveSpeed;
        }
    }
}
