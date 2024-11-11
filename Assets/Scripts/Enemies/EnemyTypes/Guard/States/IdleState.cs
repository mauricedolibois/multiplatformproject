using UnityEngine;

public class IdleState : AStateBehaviour
{

    [SerializeField] private float idleDuration = 2f;
    private float idleTimer = 0f;

    private EnemyFoV fov;
    private ImmediateDetection detection;


    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("IDLE");
        fov = GetComponent<EnemyFoV>();
        detection = GetComponentInChildren<ImmediateDetection>();
        detection.detected = false;
        idleTimer = 0f;
    }

    public override void OnStateUpdate()
    {
         idleTimer += Time.deltaTime;
            if (idleTimer >= idleDuration)
            {
                AssociatedStateMachine.SetState((int)EGuardState.Patrol); // Transition back to Patroling
            }
            // fov.suspicionLevel = lowerSuspicion(fov.suspicionLevel);
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
