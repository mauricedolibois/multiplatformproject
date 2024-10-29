using UnityEngine;
using UnityEngine.AI;

public class PatrolState : AStateBehaviour
{
    [SerializeField] private Transform[] waypoints;
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
        Debug.Log("PATROL");
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
        //add player detection here
        if (Input.GetKeyDown(KeyCode.P))
        {
            return (int)EGuardState.Suspicious;
        }

        return (int)EGuardState.Invalid; 
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
