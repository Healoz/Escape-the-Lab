using UnityEngine;

public class DeadState : State
{

    public override void Enter()
    {
        spriteRenderer.color = Color.red;
        rigidBody.linearVelocity = new Vector2(0, rigidBody.linearVelocity.y); // reset linear velocity
    }
    public override void Do()
    {

    }
    public override void Exit() { }
}
