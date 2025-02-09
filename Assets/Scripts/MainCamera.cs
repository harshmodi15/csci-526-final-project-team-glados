using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform player;
    [SerializeField] public float cameraMinX;
    [SerializeField] public float cameraMaxX;
    [SerializeField] public float playerOffset;

    void Update()
    {
        if (player.position.x - transform.position.x > playerOffset && transform.position.x < cameraMaxX)
        {
            transform.position = new Vector3(player.position.x - playerOffset, transform.position.y, transform.position.z);
        }
        else if (transform.position.x - player.position.x > playerOffset && transform.position.x > cameraMinX)
        {
            transform.position = new Vector3(player.position.x + playerOffset, transform.position.y, transform.position.z);
        }
    }
}
