using UnityEngine;

public class AirState : State
{
    public override void Enter()
    {
        spriteRenderer.color = Color.red;
    }
    public override void Do()
    {
        // end condition
        if (isGrounded)
        {
            Debug.Log("airborne done");
            isComplete = true;
        }
    }
    public override void Exit() { }
}
