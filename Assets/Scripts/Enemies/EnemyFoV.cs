using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class EnemyFoV : MonoBehaviour
{
    [SerializeField] private float fov = 90f;
    [SerializeField] private float viewDistance = 50f;
    [SerializeField] private Vector3 viewDirection; // Current direction navMeshAgent.destination - transform.position
    private GameObject player;
    private NavMeshAgent navMeshAgent;
    public float suspicionLevel = 0f;

    private Transform directionIndicator;
    [SerializeField] private float indicatorRadius = 1f;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player"); 
        directionIndicator = transform.GetChild(0);
    }
    void Update()
    {
        if (navMeshAgent.destination != Vector3.zero)
        {
            viewDirection = GetDirection();
        }
        
        LookInDirection();
        DrawDebugLines();
    }
    
    public int FindPlayerTarget()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < viewDistance) // If the player is close enough activate the code
        {
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized; // Check direction to the player
            if (Vector3.Angle(viewDirection, directionToPlayer) < fov / 2) //Check if the direction to the player is the same to the direction the enemy is facing
            {
                RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, directionToPlayer, viewDistance); // Cast a ray in the direction of the player
                if (raycastHit.collider != null)
                {
                    if (raycastHit.collider.CompareTag("Player"))
                    {
                        // If the player is directly being seen, Raises suspicion level
                        suspicionLevel = RaiseSuspicion(suspicionLevel);
                        Debug.Log($"This is the player and suspicion level is {suspicionLevel}"); 
                        if (suspicionLevel >= 100f)
                        { 
                            return (int)EGuardState.Alarmed;
                        } else if (suspicionLevel >= 50f)
                        {
                            return (int)EGuardState.Suspicious;
                        }
                    } else {
                        //It hit something else
                        Debug.Log(raycastHit.collider.name);
                        return (int)EGuardState.Invalid;
                    }
                }
            }
        }
        return (int)EGuardState.Invalid;
    }
    private Vector3 GetDirection()
    {
        Vector3 movementDirection = navMeshAgent.velocity.normalized;
        
        if (navMeshAgent.destination != transform.position)
        {
            // Update viewDirection angle to the enemies current facing direction
            return movementDirection;
        }
        // Keeps the same viewDirection angle
        return viewDirection;
    }

    private float RaiseSuspicion(float suspicion)
    {
        if (Vector3.Distance(transform.position, player.transform.position) > viewDistance / 2)
        {
            return suspicion + 15 * Time.deltaTime;
        }
        else
        {
            return suspicion + 30 * Time.deltaTime;
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
        Vector3 targetPosition = transform.position + movementDirection * indicatorRadius;

        // Update the triangle position and rotation to face the movement direction
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
}