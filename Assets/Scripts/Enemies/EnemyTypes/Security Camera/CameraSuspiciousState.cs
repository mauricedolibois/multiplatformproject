using UnityEngine;
using UnityEngine.AI;

public class CameraSuspiciousState : AStateBehaviour
{
    private EnemyFoV fov;

    public override bool InitializeState()
    {
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

