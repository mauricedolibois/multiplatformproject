using UnityEngine;
using UnityEngine.AI;

public class SuspiciousState : AStateBehaviour
{
    private Transform investigationPoint;
    [SerializeField] private float investigationDuration = 2f;
    private NavMeshAgent navMeshAgent;
    private bool isInvestigating = false;
    private float investigationTimer = 0f;

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
        isInvestigating = false;
        investigationTimer = 0f;
        navMeshAgent.SetDestination(investigationPoint.position);
    }

    public override void OnStateUpdate()
    {
        if (!isInvestigating && !navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            isInvestigating = true;
        }

        if (isInvestigating)
        {
            investigationTimer += Time.deltaTime;
            if (investigationTimer >= investigationDuration)
            {
                AssociatedStateMachine.SetState(0); // Transition back to IDLE
            }
        }
    }

    public override void OnStateEnd()
    {
        navMeshAgent.ResetPath();
    }

    public override int StateTransitionCondition()
    {
        return -1; // No transition to other states by default
    }
}
