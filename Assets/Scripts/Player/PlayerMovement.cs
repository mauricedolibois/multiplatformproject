using TreeEditor;
using Unity.VisualScripting;
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
    

    private Transform directionIndicator;
    private SpriteRenderer spriteRenderer;
    private GameObject melee;
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
        
        directionIndicator = transform.GetChild(0);
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        melee = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        HandleInput();
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
        directionIndicator.position = targetPosition;
        directionIndicator.rotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
        
        // if (Input.GetAxis("Horizontal") < 0 && scaleX >0 || Input.GetAxis("Horizontal") > 0 && scaleX < 0)
        // {
        //     scaleX *= -1;
        //     transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        //     Vector3 position = spriteRenderer.bounds.center;
        //     position.x += 0.5f * scaleX;
        //     directionIndicator.transform.position = position;
        //     meleeOffset = directionIndicator.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        //     position.x += meleeOffset * scaleX;
        //     melee.transform.position = position;
        // }
        //
        // if (Input.GetAxis("Vertical") > 0)
        // {
        //     Vector3 position = spriteRenderer.bounds.max;
        //     position.x = spriteRenderer.bounds.center.x;
        //     directionIndicator.transform.position = position;
        //     meleeOffset = directionIndicator.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        //     position.y += meleeOffset;
        //     melee.transform.position = position;
        // }
        // else if (Input.GetAxis("Vertical") < 0)
        // {
        //     Vector3 position = spriteRenderer.bounds.min;
        //     position.x = spriteRenderer.bounds.center.x;
        //     directionIndicator.transform.position = position;
        //     meleeOffset = directionIndicator.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        //     position.y -= meleeOffset;
        //     melee.transform.position = position;
        // }
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
}