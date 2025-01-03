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
    private float originalSpeed;

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

    private Animator animator;

    void Start()
    {
        originalSpeed = moveSpeed;
        
        rb = GetComponent<Rigidbody2D>();

        directionIndicator = transform.GetChild(0).transform;

        spriteRenderer = GetComponent<SpriteRenderer>();

        melee = transform.GetChild(1).gameObject.transform;

        immune = false;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
        SpeedCheat();
        PlayerImmune();
        if (isMovementAllowed)
        {
            animator.SetBool("movement_allowed", true);
            animator.SetBool("run_right", movement.x > 0);
            animator.SetBool("run_left", movement.x < 0);
            animator.SetBool("run_up", movement.y > 0);
            animator.SetBool("run_down", movement.y < 0);
        }
        else
        {
            animator.SetBool("run_right", false);
            animator.SetBool("run_left", false);
            animator.SetBool("run_up", false);
            animator.SetBool("run_down", false);
            animator.SetBool("movement_allowed", false);
        }
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

        if (Input.GetKey(KeyCode.Alpha0))
        {
            moveSpeed = originalSpeed;
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
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            transform.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
    
    public void SetMovementAllowed(bool allowed)
    {
        isMovementAllowed = allowed;
    }

    public void MeleeSpeed(bool active)
    {
        if (active)
        {
            moveSpeed *= 0.25f;
        }
        else
        {
            moveSpeed = originalSpeed;
        }
        
    }
}