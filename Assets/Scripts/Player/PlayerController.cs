using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private float maxShootingDistance = 50f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask portalLayer;

    [Header("Interaction Settings")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private LayerMask boxLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float interactionRange = 2f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;
    private bool jumpPressed;
    private ThrowableBox heldBox;
    private SpriteRenderer spriteRenderer;
    private float currentVelocityMagnitude;
    private bool isLineVisible;
    public bool fromPortal;
    public LineRenderer lineRenderer;

    private void Awake()
    {
        fromPortal = false;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (!groundCheck)
        {
            groundCheck = transform;
        }
        if (lineRenderer == null)
        {
            isLineVisible = true;
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
            lineRenderer.sortingOrder = 100;
        }
    }

    private void Update()
    {
        // Get movement input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Handle box interaction
        // if (Input.GetKeyDown(interactKey))
        // {
        //     if (heldBox == null)
        //     {
        //         TryPickupBox();
        //     }
        //     else
        //     {
        //         DropBox();
        //     }
        // }

        // Flip sprite based on movement direction
        if (horizontalInput != 0)
        {
            spriteRenderer.flipX = horizontalInput < 0;
        }
        
        currentVelocityMagnitude = rb.velocity.magnitude;

        // Draw line
        if (Input.GetKeyDown(KeyCode.F))
        {
            isLineVisible = !isLineVisible;
            lineRenderer.enabled = isLineVisible;
        }
        
        if (isLineVisible)
        {
            Vector2 start = transform.position;
            Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(start, direction, maxShootingDistance, portalLayer);
            // line rendering above all other objects
            if (hit.collider != null)
            {
                lineRenderer.SetPosition(0, start);
                lineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                lineRenderer.SetPosition(0, start);
                lineRenderer.SetPosition(1, start + direction * maxShootingDistance);
            }
        }
    }
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
    //     {
    //         // Calculate damage based on velocity
    //         float damage = currentVelocityMagnitude;
    //         // Try to apply damage to enemy
    //         Enemy enemy = collision.gameObject.GetComponent<Enemy>();
    //         if (enemy != null)
    //         {
    //             enemy.TakeDamage(damage);
    //         }
    //     }
    // }        // Check if we hit an enemy

    private void FixedUpdate()
    {
        // Handle movement
        Vector2 moveVelocity = rb.velocity;
        if (fromPortal)
        {
            moveVelocity.x = moveVelocity.x + horizontalInput * moveSpeed * 0.05f;

        }
        else
        {
            moveVelocity.x = horizontalInput * moveSpeed;
        }
        if (isGrounded && fromPortal)
        {
            fromPortal = false;
        }
        // Handle jumping
        if (jumpPressed && isGrounded)
        {
            moveVelocity.y = jumpForce;
            jumpPressed = false;
        }
        else if (!isGrounded)
        {
            // Prevent jumping in mid-air
            jumpPressed = false;
        }

        rb.velocity = moveVelocity;
    }

    private void TryPickupBox()
    {
        // Check for nearby boxes
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRange, boxLayer);
        
        foreach (Collider2D collider in colliders)
        {
            ThrowableBox box = collider.GetComponent<ThrowableBox>();
            if (box != null && box.TryPickup(transform))
            {
                heldBox = box;
                break;
            }
        }
    }

    private void DropBox()
    {
        if (heldBox != null)
        {
            // The box's ThrowBox method will be called if player clicks
            heldBox = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize ground check
        if (groundCheck)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        // Visualize interaction range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }

    public float GetCurrentVelocityMagnitude()
    {
        return currentVelocityMagnitude;
    }
}