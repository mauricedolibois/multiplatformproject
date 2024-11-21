using UnityEngine;
using UnityEngine.AI;

public class CameraSuspiciousState : AStateBehaviour
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
        EnemySignToggle signToggle = GetComponent<EnemySignToggle>();
        signToggle.ShowQuestionMark();
    }

    public override void OnStateUpdate()
    {
    }

    public override void OnStateEnd()
    {
    }

    public override int StateTransitionCondition()
    {
        return fov.FindPlayerTarget();    
    }
}

