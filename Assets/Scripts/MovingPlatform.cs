using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float minX;
    [SerializeField] public float maxX;

    private Vector3 direction = Vector3.right;

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        if (transform.position.x >= maxX)
        {
            direction = Vector3.left;
        }
        else if (transform.position.x <= minX)
        {
            direction = Vector3.right;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            collision.transform.position += direction * speed * Time.deltaTime;
        }

    }

}
