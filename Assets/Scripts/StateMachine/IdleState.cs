using UnityEngine;

public class IdleState : State
{
    public override void Enter()
    {
        spriteRenderer.color = Color.green;
        rigidBody.linearVelocity = new Vector2(0, rigidBody.linearVelocity.y);
    }
    public override void Do()
    {
        // end condition
        if (!groundColliderScript.isGrounded)
        {
            Debug.Log("idle done");
            isComplete = true;
        }
    }
    public override void Exit() { }
}
