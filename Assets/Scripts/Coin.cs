using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip[] coinSfx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            other.GetComponent<PlayerManager>().AddToScore();
            AudioSource.PlayClipAtPoint(coinSfx[Random.Range(0, coinSfx.Length)], transform.position, 1f);
            Destroy(gameObject);
        }
    }
}
