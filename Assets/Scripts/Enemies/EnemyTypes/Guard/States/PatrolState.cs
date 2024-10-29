using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : AStateBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private NavMeshAgent navMeshAgent;

    private EnemyFoV fov;

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
        Debug.Log("PATROL");
        fov = GetComponent<EnemyFoV>();
        SetNextWaypoint();
    }

    public override void OnStateUpdate()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            // 50% chance to switch to Idle state
            if (Random.Range(0, 100) < 50)
            {
                AssociatedStateMachine.SetState((int)EGuardState.Idle); // Transition to IdleState
                return;
            }
            
            SetNextWaypoint();
        }
    }

    public override void OnStateEnd()
    {
        navMeshAgent.ResetPath();
    }

    public override int StateTransitionCondition()
    {
        var seePlayer = fov.FindPlayerTarget();
        return seePlayer;
    }

    private void SetNextWaypoint()
    {
        if (waypoints.Length > 0)
        {
            int randomIndex = Random.Range(0, waypoints.Length);
            navMeshAgent.SetDestination(waypoints[randomIndex].position);
        }
    }
}
