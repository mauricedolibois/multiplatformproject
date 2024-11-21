using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class EnemyFoV : MonoBehaviour
{
    [SerializeField] private float fov = 30f;
    [SerializeField] private float viewDistance = 10f;
    [SerializeField] private Vector3 viewDirection; // Current direction navMeshAgent.destination - transform.position
    
    [SerializeField] private float suspicionFactor = 1f;

    [SerializeField] private float suspicionLimit = 25f;
    [SerializeField] private float alarmedLimit = 75f;

    [SerializeField] private float suspicionFall = 10f;
    private GameObject player;
    private NavMeshAgent navMeshAgent;
    public float suspicionLevel = 0f;

    private Transform directionIndicator;
    private Transform detectionBox;
    [SerializeField] private float indicatorRadius = 1f;
    [SerializeField] private float detectionBoxRadius = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (transform.gameObject.CompareTag("Enemy"))
        {
            directionIndicator = transform.GetChild(0).transform;
            detectionBox = transform.GetChild(1).transform;
        }
    }
    void Update()
    {
        if (navMeshAgent.velocity != Vector3.zero)
        {
            viewDirection = GetDirection();
        }

        if (suspicionLevel > 0){
            suspicionLevel -= suspicionFall/suspicionLevel * Time.deltaTime;
        } else if (suspicionLevel < 0)
        {
            suspicionLevel = 0;
        }
        
        LookInDirection();
        DrawDebugLines();
    }
    
    public int FindPlayerTarget()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > viewDistance) // If the player is too far keep current state
        {
            return CheckTypeForInvalid();
        }
        
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized; // Check direction to the player
        
        if (Vector3.Angle(viewDirection, directionToPlayer) > fov / 2) // If the direction the enemy is facing is different from the direction to the player, keep current state
        {
            return CheckTypeForInvalid();
        }
        
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, directionToPlayer, viewDistance); // Cast a ray in the direction of the player
        Debug.DrawRay(transform.position, directionToPlayer * 10, Color.blue, 10f);  
        if (raycastHit.collider.CompareTag("Player"))
        {
            // If the player is directly being seen, Raises suspicion level
            suspicionLevel = RaiseSuspicion(suspicionLevel);
            Debug.Log($"This is the player and suspicion level is {suspicionLevel}"); 
            if (suspicionLevel >= alarmedLimit)
            { 
                // Change state into alarmed
                switch (transform.gameObject.tag)
                {
                    case "Enemy":
                        return (int)EGuardState.Alarmed;
                    case "Security Camera":
                        return (int)ECameraState.Alarmed;
                }
            } if (suspicionLevel >= suspicionLimit)
            {
                // Change state into Suspicious
                switch (transform.gameObject.tag)
                {
                    case "Enemy":
                        return (int)EGuardState.Suspicious;
                    case "Security Camera":
                        return (int)ECameraState.CameraSuspiciousState;
                }
            }
        } else {
            // If there's an object other than the player, keep the current state
            return CheckTypeForInvalid();
        }
        
        // If all else fails, keep current state
        return CheckTypeForInvalid();
    }
    private Vector3 GetDirection()
    {
        return navMeshAgent.velocity.normalized;
    }

    private float RaiseSuspicion(float suspicion)
    {
        if (Vector3.Distance(transform.position, player.transform.position) > viewDistance / 2)
        {
            return suspicion + 15 * suspicionFactor * Time.deltaTime;
        }
        else
        {
            return suspicion + 25 * suspicionFactor * Time.deltaTime;
        }
    }
    
    private void LookInDirection()
    {
        Vector3 movementDirection = navMeshAgent.velocity.normalized;

        if (movementDirection == Vector3.zero)
        {
            return;
        }
        
        // Calculate the target position for the triangle based on movement direction
        Vector3 targetPosition = transform.position + movementDirection * detectionBoxRadius;

        // Update the triangle position and rotation to face the movement direction
        detectionBox.position = targetPosition;
        detectionBox.rotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            
        targetPosition = transform.position + movementDirection * indicatorRadius;
        directionIndicator.position = targetPosition;
        directionIndicator.rotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
    }

    private void DrawDebugLines()
    {
        //use the movement direction to draw the view cone
        Vector3 movementDirection = navMeshAgent.velocity.normalized;
        Debug.DrawLine(transform.position, transform.position + movementDirection *viewDistance, Color.green);
        Vector3 moveLeftDirection = Quaternion.Euler(0, 0, fov / 2) * movementDirection;
        Vector3 moveRightDirection = Quaternion.Euler(0, 0, -fov / 2) * movementDirection;
        Debug.DrawLine(transform.position, transform.position + moveLeftDirection *viewDistance, Color.green);
        Debug.DrawLine(transform.position, transform.position + moveRightDirection *viewDistance, Color.green);

        //use the current view direction to draw the view cone
        Debug.DrawLine(transform.position, transform.position + viewDirection * viewDistance, Color.red);
        Vector3 leftViewDirection = Quaternion.Euler(0, 0, fov / 2) * viewDirection;
        Vector3 rightViewDirection = Quaternion.Euler(0, 0, -fov / 2) * viewDirection;
        Debug.DrawLine(transform.position, transform.position + leftViewDirection * viewDistance, Color.red);
        Debug.DrawLine(transform.position, transform.position + rightViewDirection * viewDistance, Color.red);
    }

    private int CheckTypeForInvalid()
    {
        switch (transform.gameObject.tag)
        {
            case "Enemy":
                return (int)EGuardState.Invalid;
            case "Security Camera":
                return (int)ECameraState.Invalid;
        }
        return -1;
    }
}