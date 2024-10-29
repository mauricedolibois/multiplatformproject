using UnityEngine;

public class IdleState : AStateBehaviour
{

    [SerializeField] private float idleDuration = 2f;
    private float idleTimer = 0f;

    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("IDLE");
        idleTimer = 0f;
    }

    public override void OnStateUpdate()
    {
         idleTimer += Time.deltaTime;
            if (idleTimer >= idleDuration)
            {
                AssociatedStateMachine.SetState((int)EGuardState.Patrol); // Transition back to Patroling
            }
    }

    public override void OnStateEnd()
    {
    }

    public override int StateTransitionCondition()
    {

        //add player detection here
        return (int)EGuardState.Invalid;
    }
}