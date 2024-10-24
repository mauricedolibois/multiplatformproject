using UnityEngine;
using UnityEngine.AI;

public class IdleState : AStateBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private NavMeshAgent navMeshAgent;

    public override bool InitializeState()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (waypoints == null || waypoints.Length == 0 || navMeshAgent == null)
        {
            return false;
        }

        navMeshAgent.updateRotation = false; // Disable rotation, useful for 2D
        navMeshAgent.updateUpAxis = false;   // Essential for 2D navigation
        return true;
    }

    public override void OnStateStart()
    {
        currentWaypointIndex = 0;
        SetNextWaypoint();
    }

    public override void OnStateUpdate()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            SetNextWaypoint();
        }
    }

    public override void OnStateEnd()
    {
        navMeshAgent.ResetPath();
    }

    public override int StateTransitionCondition()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            return 1; // Switch to SuspiciousState
        }

        return -1; // No transition
    }

    private void SetNextWaypoint()
    {
        if (waypoints.Length > 0)
        {
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }
}
