using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
