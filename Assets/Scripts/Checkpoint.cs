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
    [SerializeField] private AudioClip passSfx;
    [SerializeField] private ParticleSystem passVfx;

    private AudioSource cpAudio;

    private void Start()
    {
        cpAudio = GetComponent<AudioSource>();
        coinReqText.text = coinReq.ToString();
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            if (other.gameObject.GetComponent<PlayerManager>().GetCoinsCollected() >= coinReq)
            {
                other.gameObject.GetComponent<PlayerManager>().GoalPassed();
                cpAudio.PlayOneShot(passSfx, 0.2f);
                passVfx.Play();
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
