using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private int coinReq = 0;
    [SerializeField] private TextMeshPro coinReqText;
    [SerializeField] private float zPosToRespawnPlayer = 0;
    [SerializeField] private float secondsBeforeRespawn = 2f;

    private void Start()
    {
        coinReqText.text = coinReq.ToString();
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            if (other.gameObject.GetComponent<PlayerManager>().GetCoinsCollected() >= coinReq)
            {
                other.gameObject.GetComponent<PlayerManager>().GoalPassed();
                zPosToRespawnPlayer = transform.position.z;
                GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovement>().GoalFailed();
                StartCoroutine(RespawnAtLastCheck(other));
            }
        }
    }

    private IEnumerator RespawnAtLastCheck(Collider obj)
    {
        yield return new WaitForSeconds(secondsBeforeRespawn);
        obj.gameObject.transform.position = new Vector3(1, 1, zPosToRespawnPlayer);
    }
}
