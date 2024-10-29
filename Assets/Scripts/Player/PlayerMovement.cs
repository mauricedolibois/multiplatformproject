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

    public OnShiftPressed onShiftPressedEvent = new(); 
    public OnShiftReleased onShiftReleasedEvent = new(); 

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isMovementAllowed = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        }
        else
        {
            rb.velocity = Vector2.zero; 
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