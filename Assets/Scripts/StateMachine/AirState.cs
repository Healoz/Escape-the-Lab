using UnityEngine;

public class AirState : State
{
    public float jumpStrength;
    public override void Enter()
    {
        isGrounded = false;
        rigidBody.linearVelocityY = jumpStrength;
    }

    public override void Do() { }

    public override void Exit() { }
}
