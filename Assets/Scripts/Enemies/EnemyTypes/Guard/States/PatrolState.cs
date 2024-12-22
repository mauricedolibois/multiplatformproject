using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : AStateBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private NavMeshAgent navMeshAgent;

    private Animator animator;
    private EnemyFoV fov;
    private ImmediateDetection detection;

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
        animator = GetComponent<Animator>();
        fov = GetComponent<EnemyFoV>();
        detection = GetComponentInChildren<ImmediateDetection>();
        detection.detected = false;
        EnemySignToggle signToggle = GetComponent<EnemySignToggle>();
        signToggle.HideSigns();
        SetNextWaypoint();
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
            // 50% chance to switch to Idle state
            if (Random.Range(0, 100) < 50)
            {
                AssociatedStateMachine.SetState((int)EGuardState.Idle); // Transition to IdleState
                return;
            }
            
            SetNextWaypoint();
            
            // fov.suspicionLevel = lowerSuspicion(fov.suspicionLevel);
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
    
    private void SetNextWaypoint()
    {
        if (waypoints.Length > 0)
        {
            int randomIndex = Random.Range(0, waypoints.Length);
            navMeshAgent.SetDestination(waypoints[randomIndex].position);
        }
    }
    
    // private float lowerSuspicion(float suspicion)
    // {
    //     if (suspicion > 0f)
    //     {
    //         return suspicion - 15 * Time.deltaTime;
    //     }
    //     return suspicion;
    // }
}
