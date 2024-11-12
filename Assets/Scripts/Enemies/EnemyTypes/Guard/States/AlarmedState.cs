using UnityEngine;

public class AlarmedState : AStateBehaviour
{
    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("ALARMED");
        EnemySignToggle signToggle = GetComponent<EnemySignToggle>();
        signToggle.ShowExclamationMark();
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
        return (int)EGuardState.Invalid; // Default: no transition
    }
}
