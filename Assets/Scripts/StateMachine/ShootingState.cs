using UnityEngine;

public class ShootingState : State
{
    public BackAwayState backAwayState;
    public IdleState idleState;
    public ShootState shootState;
    public float shotIntervalInSeconds;
    public GameObject target;
    public float distanceFromTarget;
    public float distanceToBackAway;

    public override void Enter()
    {
        spriteRenderer.color = Color.cyan;

        Set(shootState, true);
    }
    public override void Do()
    {

        GetDistanceFromTarget();

        if (distanceFromTarget < distanceToBackAway) // back away if player too close
        {
            Set(backAwayState);
        }
        else
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


    }

    public void GetDistanceFromTarget()
    {
        distanceFromTarget = Vector2.Distance(transform.position, target.transform.position);
    }


    public override void Exit() { }
}
