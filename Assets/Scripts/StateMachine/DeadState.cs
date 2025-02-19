using UnityEngine;

public class DeadState : State
{

    public override void Enter()
    {

        rigidBody.linearVelocity = new Vector2(0, rigidBody.linearVelocity.y); // reset linear velocity
        spriteRenderer.color = Color.red;
    }
    public override void Do()
    {

    }
    public override void Exit()
    {
        spriteRenderer.color = Color.yellow;
    }
}
