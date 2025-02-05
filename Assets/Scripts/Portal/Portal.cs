using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Portal Settings")]
    [SerializeField] private Color bluePortalColor = Color.blue;
    [SerializeField] private Color orangePortalColor = new Color(1f, 0.5f, 0f, 1f);
    [SerializeField] private float teleportCooldown = 0.5f;

    private SpriteRenderer spriteRenderer;
    private PortalType type;
    private Portal linkedPortal;
    private float lastTeleportTime;

    public PortalType Type => type;
    public Portal LinkedPortal
    {
        get => linkedPortal;
        set => linkedPortal = value;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(PortalType portalType)
    {
        type = portalType;
        UpdatePortalColor();
    }

    private void UpdatePortalColor()
    {
        if (spriteRenderer == null) return;
        spriteRenderer.color = type == PortalType.Blue ? bluePortalColor : orangePortalColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (linkedPortal == null) return;
        if (Time.time - lastTeleportTime < teleportCooldown) return;

        // Check if the object can be teleported
        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            TeleportObject(other);
        }
    }

    private void TeleportObject(Collider2D other)
    {
        // Get the object's rigidbody
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        // Calculate teleport position
        Vector2 enterVelocity = rb.velocity;
        Vector2 teleportPosition = linkedPortal.transform.position;

        // Preserve momentum through portal
        Vector2 exitVelocity = enterVelocity;
        // TODO: Add rotation logic based on portal orientations
        

        // Teleport the object
        other.transform.position = teleportPosition;
        rb.velocity = exitVelocity;

        // Update teleport cooldown
        lastTeleportTime = Time.time;
        linkedPortal.lastTeleportTime = Time.time;
    }
}