using UnityEngine;

public class CameraIdleState : AStateBehaviour
{
    private EnemyFoV fov;
    
    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        fov = GetComponent<EnemyFoV>();
        EnemySignToggle signToggle = GetComponent<EnemySignToggle>();
        signToggle.HideSigns();
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