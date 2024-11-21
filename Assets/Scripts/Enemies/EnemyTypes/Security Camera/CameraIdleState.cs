using UnityEngine;

public class CameraIdleState : AStateBehaviour
{
    private float idleTimer = 0f;
    private float idleDuration = 1f;
    
    
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

    // private float lowerSuspicion(float suspicion)
    // {
    //     if (suspicion > 0f)
    //     {
    //         return suspicion - 15 * Time.deltaTime;
    //     }
    //     return suspicion;
    // }
}