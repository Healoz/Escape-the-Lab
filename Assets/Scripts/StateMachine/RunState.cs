using UnityEngine;

public class RunState : State
{
    public float runSpeed;
    public override void Enter()
    {
        spriteRenderer.color = Color.cyan;
    }
    public override void Do()
    {
        //  end condition
        if (!input.isGrounded)
        {
            Debug.Log("run done");
            isComplete = true;
        }
    }
    public override void Exit() { }
}
