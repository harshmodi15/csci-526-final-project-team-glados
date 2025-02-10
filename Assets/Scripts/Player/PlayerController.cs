using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

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
    public bool fromPortal;

    private void Awake()
    {
        fromPortal = false;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (!groundCheck)
        {
            groundCheck = transform;
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
        if (Input.GetKeyDown(interactKey))
        {
            if (heldBox == null)
            {
                TryPickupBox();
            }
            else
            {
                DropBox();
            }
        }

        // Flip sprite based on movement direction
        if (horizontalInput != 0)
        {
            spriteRenderer.flipX = horizontalInput < 0;
        }
        
        currentVelocityMagnitude = rb.velocity.magnitude;

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