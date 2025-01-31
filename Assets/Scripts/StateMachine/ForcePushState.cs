using UnityEngine;

public class ForcePushState
 : State
{
    public float attackAmount;
    public float pushForce;
    public override void Enter()
    {

        spriteRenderer.color = Color.yellow;
    }
    public override void Do()
    {
        // end condition here

    }
    public override void Exit() { }
}
