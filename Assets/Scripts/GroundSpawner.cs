using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] GameObject groundToSpawn;
    [SerializeField] GameObject player;
    [SerializeField] private float zLocToSpawn = 50f;
    [SerializeField] private float zLocIncrement = 50f;
    [SerializeField] private float zSpawnTrigger = 0f;
    [SerializeField] private float zDespawnTrigger = 50f;

    private Queue<GameObject> groundObjectsInScene = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        QueueFirstGrounds();
    }

    private void QueueFirstGrounds()
    {
        Vector3 spawnPos = new Vector3(0f, 0f, zLocToSpawn);
        GameObject startGround = Instantiate(groundToSpawn, spawnPos, Quaternion.identity);
        groundObjectsInScene.Enqueue(startGround);
        zLocToSpawn += zLocIncrement;
        zSpawnTrigger = zLocIncrement;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 spawnPos = new Vector3(0f, 0f, zLocToSpawn);
        if (player.transform.position.z > zSpawnTrigger)
        {
            GameObject newGround = Instantiate(groundToSpawn, spawnPos, Quaternion.identity);
            groundObjectsInScene.Enqueue(newGround);

            zLocToSpawn += zLocIncrement;
            zSpawnTrigger += zLocIncrement;
        }
        //-0.03403497
        //-0.035
        if (player.transform.position.z > zDespawnTrigger)
        {
            zDespawnTrigger += 50f;
            Destroy(groundObjectsInScene.Dequeue());
        }
    }
}
