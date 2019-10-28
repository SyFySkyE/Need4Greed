using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private float backSpeed = -2f;
    [SerializeField] private EnemyBoss dragon;
    [SerializeField] private float moveTowardMotherSpeed = 10f;
    [SerializeField] private float secondsBeforeDestroy = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, secondsBeforeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        if (dragon == null)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * backSpeed);
        }
        else
        {
            float moveStep = moveTowardMotherSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, dragon.transform.position, moveStep);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.layer = 9;        
    }

    private void OnCollisionEnter(Collision collision)
    {
        dragon = FindObjectOfType<EnemyBoss>();        
        this.gameObject.layer = 9;        
        dragon.GetComponent<EnemyBoss>().StateOneTwoTransition();
        
    }
}
