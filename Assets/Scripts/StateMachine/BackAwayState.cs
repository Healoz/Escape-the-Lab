using UnityEngine;

public class BackAwayState : State
{

    public override void Enter()
    {
        spriteRenderer.color = Color.blue;
    }
    public override void Do()
    {
        //  end condition here
    }

    public override void Exit() { }
}
