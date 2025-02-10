using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float damage = collision.GetComponent<PlayerController>().GetCurrentVelocityMagnitude();
            transform.parent.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
