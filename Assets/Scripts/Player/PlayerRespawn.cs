using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Collision detected with: " + other.gameObject.name);
        
        if (other.CompareTag("Trap")) 
        {
            Respawn();
        }
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     Debug.Log("Collision detected with: " + collision.gameObject.name); // Debugging
    //     if (collision.gameObject.CompareTag("Trap"))
    //     {
    //         Respawn();
    //     }
    // }

    void Respawn()
    {
        transform.position = startPosition;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
}