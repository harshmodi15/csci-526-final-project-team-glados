using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float fallThreshold = -10f;
    private Vector2 startPosition;
    
    [HideInInspector] public bool PlayerSteppedOnHead = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Collision detected with: " + other.gameObject.name);
        
        if (other.CompareTag("Trap")) 
        {
            Respawn();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Hostility"))
        {
            if (collision.gameObject.GetComponent<HeadTrigger>() == null)
            {
                Respawn();
            }
        }
    }

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