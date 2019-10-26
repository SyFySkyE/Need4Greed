using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private GameObject player;
    [SerializeField] private float yPos = 10;
    [SerializeField] private float zPos = 25f;
    [SerializeField] private float zDistanceToSpawn = 50f;
    [SerializeField] private float xConstraint = 5f;
    [SerializeField] private float moveSpeed = 3f;

    [Header("Prefabs To Spawn")]
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject safeFireBall;
    [SerializeField] private GameObject enemy;

    [Header("Spawn Parameters")]
    [SerializeField] private float timeBetweenSpawns = 1f;
    [SerializeField] private int chanceOfSpawningSafe = 7;
    [SerializeField] GameObject spawnLocation;

    private enum BossState { Despawned, Spawning, Spawned, Moving, Firing, Dead}
    private BossState currentState;

    private enum PhaseState { ZeroOne, PhaseOne, OneTwo, PhaseTwo, TwoThree, PhaseThree }
    private PhaseState currentPhase;

    // Start is called before the first frame update
    void Start()
    {
        currentPhase = PhaseState.ZeroOne;
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

        switch (currentPhase)
        {
            case PhaseState.ZeroOne:
                InvokeRepeating("SpawnFireBalls", timeBetweenSpawns, timeBetweenSpawns);
                currentPhase = PhaseState.PhaseOne;
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

    private void SpawnFireBalls()
    {
        int r = Random.Range(0, chanceOfSpawningSafe);
        if (r == 0)
        {
            Instantiate(safeFireBall, spawnLocation.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(fireBall, spawnLocation.transform.position, Quaternion.identity);
        }
    }
}
