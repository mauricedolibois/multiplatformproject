using UnityEngine;

public class CompanionIdle : AStateBehaviour
{
    private EnemyFoV fov;
    private ImmediateDetection detection;

    private Animator animator;
    
    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("COMPANION IDLE");
        animator = GetComponent<Animator>();
        animator.SetBool("run_right", false);
        animator.SetBool("run_left", false);
        animator.SetBool("run_up", false);
        animator.SetBool("run_down", false);
        fov = GetComponent<EnemyFoV>();
        detection = GetComponentInChildren<ImmediateDetection>();
        detection.detected = false;
        EnemySignToggle signToggle = GetComponent<EnemySignToggle>();
        signToggle.HideSigns();
    }

    public override void OnStateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            AssociatedStateMachine.SetState((int)EGuardState.Patrol);
        }
    }

    public override void OnStateEnd()
    {
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
    
    // private float lowerSuspicion(float suspicion)
    // {
    //     if (suspicion > 0f)
    //     {
    //         return suspicion - 15 * Time.deltaTime;
    //     }
    //     return suspicion;
    // }
}