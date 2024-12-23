using System;
using System.Collections;
using UnityEngine;

public class CompanionIdle : AStateBehaviour
{
    private EnemyFoV fov;
    private ImmediateDetection detection;

    private RestAPI restAPI;
    private bool isCheckingInput;
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

        StartCoroutine(CheckInputPeriodically());
    }

    public override void OnStateUpdate()
    {
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
    
    private IEnumerator CheckInputPeriodically()
    {
        while (isCheckingInput)
        {
            if (restAPI != null)
            {
                // Call CheckInput method and handle the response
                yield return StartCoroutine(restAPI.CheckInput(result =>
                {
                    if (result)
                    {
                        AssociatedStateMachine.SetState((int)EGuardState.Patrol);
                        isCheckingInput = false;
                    }
                }));
            }

            yield return new WaitForSeconds(2f); 
        }
    }
}