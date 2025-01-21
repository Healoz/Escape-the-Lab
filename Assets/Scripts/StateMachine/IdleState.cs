using UnityEngine;

public class IdleState : State
{
    public override void Enter()
    {
        spriteRenderer.color = Color.green;
    }
    public override void Do()
    {
        // end condition
        if (rigidBody.linearVelocityX != 0 || !isGrounded)
        {
            Debug.Log("idle done");
            isComplete = true;
        }
    }
    public override void Exit() { }
}
