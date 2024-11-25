using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]

public class OnShiftPressed : UnityEvent { }
public class OnShiftReleased : UnityEvent { }

public class PlayerMovement : Singleton<PlayerMovement>
{
    public static class Constants
    {
        public const string HorizontalInput = "Horizontal";
        public const string VerticalInput = "Vertical";
    }

    [SerializeField] private float moveSpeed = 1.0f;

    private bool immune;

    private Transform directionIndicator;
    private SpriteRenderer spriteRenderer;
    private Transform melee;
    private float meleeOffset;
    [SerializeField] private float indicatorRadius = 1f;

    public OnShiftPressed onShiftPressedEvent = new();
    public OnShiftReleased onShiftReleasedEvent = new();
    
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isMovementAllowed = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        directionIndicator = transform.GetChild(0).transform;

        spriteRenderer = GetComponent<SpriteRenderer>();

        melee = transform.GetChild(1).gameObject.transform;

        immune = false;
    }

    void Update()
    {
        HandleInput();
        SpeedCheat();
        PlayerImmune();
    }

    void FixedUpdate()
    {
        if (isMovementAllowed)
        {
            rb.velocity = movement * moveSpeed;
            LookInDirection();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void LookInDirection()
    {
        // float scaleX = transform.localScale.x;
        // float scaleY = transform.localScale.y;
        // float scaleZ = transform.localScale.z;

        Vector3 movementDirection = rb.velocity.normalized;

        if (movement == Vector2.zero)
        {
            return;
        }

        // Calculate the target position for the triangle based on movement direction
        Vector3 targetPosition = transform.position + movementDirection * indicatorRadius;

        // Update the triangle position and rotation to face the movement direction
        melee.position = targetPosition;
        directionIndicator.position = targetPosition;
        directionIndicator.rotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isMovementAllowed = false;
            onShiftPressedEvent.Invoke();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isMovementAllowed = true;
            onShiftReleasedEvent.Invoke();
        }

        MovementInput();
    }

    private void MovementInput()
    {
        movement.x = Input.GetAxisRaw(Constants.HorizontalInput);
        movement.y = Input.GetAxisRaw(Constants.VerticalInput);
        movement = movement.normalized;
    }

    // Cheat codes
    private void SpeedCheat()
    {
        if (Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus))
        {
            moveSpeed += 0.1f;
        } else if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus))
        {
            moveSpeed -= 0.1f;
        }
    }

    private void PlayerImmune()
    {
        if (Input.GetKeyDown(KeyCode.K) && !immune)
        {
            immune = true;
            spriteRenderer.color = Color.yellow;
            transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        } else if (Input.GetKeyDown(KeyCode.K) && immune)
        {
            immune = false;
            spriteRenderer.color = new Color(0.3841506f, 0.7295597f, 0.3693682f, 1f);
            transform.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
    
    public void SetMovementAllowed(bool allowed)
    {
        isMovementAllowed = allowed;
    }
}