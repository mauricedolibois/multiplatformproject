using UnityEngine;

public class AlarmedState : AStateBehaviour
{
    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("Guard is alarmed!");
    }

    public override void OnStateUpdate()
    {
        // Handle alarm behavior
    }

    public override void OnStateEnd()
    {
        // Cleanup if necessary
    }

    public override int StateTransitionCondition()
    {
        return -1; // Default: no transition
    }
}
