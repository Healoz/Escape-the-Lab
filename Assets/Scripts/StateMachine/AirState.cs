using UnityEngine;

public class AirState : State
{
    public float jumpStrength;
    public override void Enter()
    {
        spriteRenderer.color = Color.red;
    }
    public override void Do()
    {
        // end condition
        if (input.isGrounded)
        {
            Debug.Log("airborne done");
            isComplete = true;
        }
    }
    public override void Exit() { }
}
