using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponentInParent<EnemyController>() != null) return;
        if (collision.CompareTag("Player"))
        {
            float damage = collision.GetComponent<PlayerController>().GetCurrentVelocityMagnitude();
            transform.parent.GetComponent<Enemy>().TakeDamage(damage);

            Debug.Log("Player hit the enemy's head and did NOT die.");
        }
        // Kill RedEnemy instantly if hit by Box
        else if (collision.CompareTag("Box")) 
        {
            Enemy enemy = transform.parent.GetComponent<Enemy>();
            Rigidbody2D boxRb = collision.GetComponent<Rigidbody2D>();
            // Instantly kill RedEnemy

            // if (enemy is RedEnemy) 
            // {
            //     Debug.Log("RedEnemy hit on head by box! Instantly killing.");
            //     // High damage ensures instant kill
            //     enemy.TakeDamage(9999f);
            // }
            // // Normal enemy takes normal damage
            // else 
            // {
            //     float damage = collision.GetComponent<Rigidbody2D>().velocity.magnitude;
            //     enemy.TakeDamage(damage);
            // }

            if (boxRb != null)
            {
                float boxSpeed = boxRb.velocity.magnitude;
                float requiredSpeed = 10f;

                Debug.Log("Box speed: " + boxSpeed);

                // Only kill RedEnemy if the Box is moving fast enough
                if (enemy is RedEnemy) 
                {
                    if (boxSpeed >= requiredSpeed) 
                    {
                        Debug.Log("RedEnemy hit on head by high-speed box! Instantly dying.");
                        enemy.TakeDamage(9999f); 
                    }
                    else
                    {
                        Debug.Log("Box hit RedEnemy but was too slow!");
                    }
                }
            }
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
