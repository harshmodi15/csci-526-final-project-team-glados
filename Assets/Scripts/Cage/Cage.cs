using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    private bool isCaptured;
    private GameObject capturedObject;
    private float lastReleaseTime;
    private float releaseCooldown = 0.5f;
    public Vector2 normal { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        isCaptured = false;
        capturedObject = null;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isCaptured || Time.time - lastReleaseTime < releaseCooldown) return;
        if (other.CompareTag("Box") || other.CompareTag("Hostility"))
        {
            // destroy the box and clone it to the cage
            capturedObject = Instantiate(other.gameObject);
            Destroy(other.gameObject);
            capturedObject.SetActive(false);
            isCaptured = true;
        }
    }

    public void Release()
    {
        if (!isCaptured) return;
        lastReleaseTime = Time.time;
        capturedObject.transform.position = transform.position;
        capturedObject.SetActive(true);
        capturedObject = null;
        isCaptured = false;
    }
    
}
