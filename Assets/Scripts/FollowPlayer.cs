using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour // TODO This is a bit of a mess
{
    [SerializeField] GameObject playerToFollow;
    [SerializeField] GameObject skyBox;
    [SerializeField] GameObject mainTerrain;
    [SerializeField] Vector3 terrainOffset = new Vector3(0f, -10f, 464f);
    [SerializeField] Vector3 skyBoxOffset = new Vector3(-10f, -67f, 316f);
    [SerializeField] Vector3 coinOffset = new Vector3(0, 0, 0);
    [SerializeField] private float yOffset = 5f;
    [SerializeField] private float zOffset = -6f;    

    // Update is called once per frame
    void Update()
    {
        mainTerrain.transform.position = transform.position + terrainOffset;
        skyBox.transform.position = transform.position + skyBoxOffset;
        float adjustedZ = playerToFollow.transform.position.z + zOffset;
        Vector3 adjustedPos = new Vector3(transform.position.x, yOffset, adjustedZ);
        transform.position = adjustedPos;
    }
}
