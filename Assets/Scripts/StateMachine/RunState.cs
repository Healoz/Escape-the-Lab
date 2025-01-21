using UnityEngine;

public class RunState : State
{
    public override void Enter()
    {
        spriteRenderer.color = Color.cyan;
    }
    public override void Do()
    {
        //  end condition
        if (rigidBody.linearVelocityX == 0 || !isGrounded)
        {
            Debug.Log("run done");
            isComplete = true;
        }
    }
    public override void Exit() { }
}
