using UnityEngine;
using UnityEngine.AI;

public class SuspiciousState : AStateBehaviour
{
    private Transform investigationPoint;
    private NavMeshAgent navMeshAgent;

    private EnemyFoV fov;


    public override bool InitializeState()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        investigationPoint = GameObject.FindGameObjectWithTag("Player").transform;
        if (investigationPoint == null || navMeshAgent == null)
        {
            return false;
        }

        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("SUSPICIOUS");
        fov = GetComponent<EnemyFoV>();
        navMeshAgent.SetDestination(investigationPoint.position);
    }

    public override void OnStateUpdate()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            AssociatedStateMachine.SetState((int)EGuardState.Idle);
        }
    }

    public override void OnStateEnd()
    {
        navMeshAgent.ResetPath();
    }

    public override int StateTransitionCondition()
    {
        var seePlayer = fov.FindPlayerTarget();
        if (seePlayer == (int)EGuardState.Alarmed)
        {
           return seePlayer;
        }
        return (int)EGuardState.Invalid;
    }
}
