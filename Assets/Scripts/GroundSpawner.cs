using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] GameObject groundToSpawn;

    [SerializeField] GameObject player;
    [SerializeField] private float zLocationSpawnTrigger = 6f;
    [SerializeField] private float zLocationToSpawn = 6f;
    [SerializeField] private float zLocationToDespawn = 50f;
    private Queue<GameObject> groundObjectsInScene = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        QueueFirstGround();
    }

    private void QueueFirstGround()
    {
        Vector3 spawnPos = new Vector3(0f, 0f, zLocationToSpawn);
        GameObject startGround = Instantiate(groundToSpawn, spawnPos, Quaternion.identity);
        groundObjectsInScene.Enqueue(startGround);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 spawnPos = new Vector3(0f, 0f, zLocationToSpawn);
        if (player.transform.position.z > zLocationSpawnTrigger)
        {
            GameObject newGround = Instantiate(groundToSpawn, spawnPos, Quaternion.identity);
            groundObjectsInScene.Enqueue(newGround);
            
            zLocationSpawnTrigger += 50f;
            zLocationToSpawn += 50f;
        }

        if(player.transform.position.z > zLocationToDespawn)
        {
            zLocationToDespawn += 50f;
            Destroy(groundObjectsInScene.Dequeue());
        }
    }
}
