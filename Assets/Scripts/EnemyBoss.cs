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
    [SerializeField] private float chargeSpeed = 25f;
    [SerializeField] private float chargeTime = 5f;
    [SerializeField] private float yPosWhenCharging = -1f;

    [Header("Prefabs To Spawn")]
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject safeFireBall;
    [SerializeField] private GameObject badDragon;
    [SerializeField] private GameObject goodDragon;

    [Header("Spawn Parameters")]
    [SerializeField] private float timeBetweenSpawns = 1f;
    [SerializeField] private int chanceOfSpawningSafe = 7;
    [SerializeField] GameObject spawnLocation;

    [Header("State Parameters")]
    [SerializeField] private float secondsBetweenStateChange = 3f;
    [SerializeField] private float secBeforeHurtAnim = 3f;

    private enum BossState { Despawned, Spawning, Spawned, Moving, Firing, Dead, Charging }
    private BossState currentState;

    private enum PhaseState { ZeroOne, PhaseOne, OneTwo, PhaseTwo, TwoThree, PhaseThree }
    private PhaseState currentPhase;

    private Animator bossAnim;
    private bool isCharging = false;

    // Start is called before the first frame update
    void Start()
    {
        currentState = BossState.Moving;
        bossAnim = GetComponent<Animator>();
        currentPhase = PhaseState.ZeroOne;
    }

    // Update is called once per frame
    void Update()
    {
        HandleStates();
        Spawn();
        Debug.Log(currentState);
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
            case BossState.Charging:               
                
                break;
        }

        switch (currentPhase)
        {
            case PhaseState.ZeroOne:
                InvokeRepeating("SpawnFireBalls", timeBetweenSpawns, timeBetweenSpawns);
                currentPhase = PhaseState.PhaseOne;
                break;
            case PhaseState.OneTwo:
                InvokeRepeating("SpawnDragons", timeBetweenSpawns, timeBetweenSpawns);
                currentPhase = PhaseState.PhaseTwo;
                break;
            case PhaseState.TwoThree:
                InvokeRepeating("Charge", chargeTime, chargeTime);
                currentPhase = PhaseState.PhaseThree;
                break;
        }
    }

    private void Spawn()
    {        
        if (transform.position.z - player.transform.position.z <= zDistanceToSpawn)
        {
            
        }
    }

    private void Move()
    {        
        if (currentState != BossState.Charging)
        {
            transform.position = new Vector3(transform.position.x, yPos, zPos + player.transform.position.z);
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            if (transform.position.x >= xConstraint || transform.position.x <= -xConstraint)
            {
                moveSpeed = -moveSpeed;
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x, yPosWhenCharging, transform.position.z);
        }
    }

    private void SpawnFireBalls()
    {
        if (currentPhase == PhaseState.PhaseOne)
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

    private void SpawnDragons()
    {
        if (currentPhase == PhaseState.PhaseTwo)
        {
            int r = Random.Range(0, chanceOfSpawningSafe);
            if (r == 0)
            {
                Instantiate(goodDragon, spawnLocation.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(badDragon, spawnLocation.transform.position, Quaternion.identity);
            }
        }        
    }

    private void Charge()
    {        
        currentState = BossState.Charging;
        bossAnim.SetTrigger("Charge");
        StartCoroutine(SwitchToMoving());
    }

    private IEnumerator SwitchToMoving()
    {
        yield return new WaitForSeconds(chargeTime);
        currentState = BossState.Moving;
        yield return new WaitForSeconds(chargeTime);
        currentState = BossState.Charging;
    }

    public void StateOneTwoTransition()
    {
        StartCoroutine(SwitchToOneTwo());
    }

    private IEnumerator SwitchToOneTwo()
    {
        yield return new WaitForSeconds(secBeforeHurtAnim);
        bossAnim.SetTrigger("Hurt");
        currentPhase = PhaseState.OneTwo;
        yield return new WaitForSeconds(secondsBetweenStateChange);
    }

    public void StateTwoThreeTransition()
    {
        StartCoroutine(SwitchToTwoThree());
    }

    private IEnumerator SwitchToTwoThree()
    {
        yield return new WaitForSeconds(secBeforeHurtAnim);
        bossAnim.SetTrigger("Hurt");
        currentPhase = PhaseState.TwoThree;
        yield return new WaitForSeconds(secondsBetweenStateChange);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bossAnim.SetTrigger("Hurt");
            currentState = BossState.Dead;
            Destroy(this.gameObject, 1f);
        }
    }
}
