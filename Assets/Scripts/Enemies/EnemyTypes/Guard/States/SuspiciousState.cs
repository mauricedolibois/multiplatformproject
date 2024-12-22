using UnityEngine;
using UnityEngine.AI;

public class SuspiciousState : AStateBehaviour
{
    private Transform investigationPoint;
    private NavMeshAgent navMeshAgent;

    private EnemyFoV fov;

    private Animator animator;
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
        animator = GetComponent<Animator>();
        fov = GetComponent<EnemyFoV>();
        detection = GetComponentInChildren<ImmediateDetection>();
        detection.detected = false;
        navMeshAgent.SetDestination(investigationPoint.position);
        EnemySignToggle signToggle = GetComponent<EnemySignToggle>();
        signToggle.ShowQuestionMark();
    }

    public override void OnStateUpdate()
    {       
        Vector3 tmp_nav = navMeshAgent.velocity;

        if (tmp_nav.magnitude > 0.1f) // Threshold to ignore very small movements
        {
            Vector3 normalizedVelocity = tmp_nav.normalized;

            animator.SetBool("run_up", normalizedVelocity.y > 0.5f);
            animator.SetBool("run_down", normalizedVelocity.y < -0.5f);
            animator.SetBool("run_right", normalizedVelocity.x > 0.5f);
            animator.SetBool("run_left", normalizedVelocity.x < -0.5f);
        }
        else
        {
            // Reset all movement animations when there is no significant velocity
            animator.SetBool("run_up", false);
            animator.SetBool("run_down", false);
            animator.SetBool("run_right", false);
            animator.SetBool("run_left", false);
        }

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
