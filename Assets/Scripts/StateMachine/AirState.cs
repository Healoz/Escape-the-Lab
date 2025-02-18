using UnityEngine;

public class AirState : State
{
    public float jumpStrength;
    public override void Enter()
    {

    }
    public override void Do()
    {
        // end condition
        if (groundColliderScript.isGrounded)
        {
            Debug.Log("airborne done");
            isComplete = true;
        }
    }
    public override void Exit() { }
}
