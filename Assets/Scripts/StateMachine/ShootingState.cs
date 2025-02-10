using UnityEngine;

public class ShootingState : State
{
    public IdleState idleState;
    public ShootState shootState;
    public float shotIntervalInSeconds;

    public override void Enter()
    {
        spriteRenderer.color = Color.cyan;

        Set(shootState, true);
    }
    public override void Do()
    {
        //  end condition here - no end condition yet
        if (state == shootState)
        { // 
            if (shootState.isComplete)
            {
                Set(idleState);
            }
        }
        else
        {
            if (idleState.time > shotIntervalInSeconds)
            {
                Set(shootState);
            }
        }

    }


    public override void Exit() { }
}
