using UnityEngine;
using UnityEngine.AI;

public class SuspiciousState : AStateBehaviour
{
    private Transform investigationPoint;
    private NavMeshAgent navMeshAgent;

    private EnemyFoV fov;
    private ImmediateDetection detection;


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
        detection = GetComponentInChildren<ImmediateDetection>();
        detection.detected = false;
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
        if (fov.FindPlayerTarget() != (int)EGuardState.Invalid)
        {
            return fov.FindPlayerTarget();    
        }
        else if (detection.detected)
        {
            return (int)EGuardState.Alarmed;
        }
        return (int)EGuardState.Invalid;
    }
}
