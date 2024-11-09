using TreeEditor;
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
    
    private int scaleX = 1;
    private int scaleY = 1;
    private GameObject FacingTriangle;
    private SpriteRenderer spriteRenderer;
    private GameObject melee;
    private float meleeOffset;

    public OnShiftPressed onShiftPressedEvent = new(); 
    public OnShiftReleased onShiftReleasedEvent = new(); 

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isMovementAllowed = true;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        FacingTriangle = transform.GetChild(0).gameObject;
        
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
        if (movement == Vector2.zero)
        {
            return;
        }

        if (Input.GetAxis("Horizontal") < 0 && scaleX == 1 || Input.GetAxis("Horizontal") > 0 && scaleX == -1)
        {
            scaleX *= -1;
            transform.localScale = new Vector3(scaleX, scaleY, 1);
            Vector3 position = spriteRenderer.bounds.center;
            position.x += 0.5f * scaleX;
            FacingTriangle.transform.position = position;
            meleeOffset = FacingTriangle.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            position.x += meleeOffset * scaleX;
            melee.transform.position = position;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            Vector3 position = spriteRenderer.bounds.max;
            position.x = spriteRenderer.bounds.center.x;
            FacingTriangle.transform.position = position;
            meleeOffset = FacingTriangle.GetComponent<SpriteRenderer>().bounds.size.y / 2;
            position.y += meleeOffset;
            melee.transform.position = position;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            Vector3 position = spriteRenderer.bounds.min;
            position.x = spriteRenderer.bounds.center.x;
            FacingTriangle.transform.position = position;
            meleeOffset = FacingTriangle.GetComponent<SpriteRenderer>().bounds.size.y / 2;
            position.y -= meleeOffset;
            melee.transform.position = position;
        }
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