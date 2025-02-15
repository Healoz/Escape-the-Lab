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
    public float nextShotTime;

    public override void Enter()
    {
        spriteRenderer.color = Color.cyan;
        nextShotTime = Time.time;
        Set(idleState, true);
    }
    public override void Do()
    {

        GetDistanceFromTarget();

        if (distanceFromTarget < distanceToBackAway) // back away if player too close
        {
            Set(backAwayState);
            return;
        }


        // Only shoot if enough time has passed since last shot
        if (Time.time >= nextShotTime)
        {
            if (state == idleState)
            {
                Set(shootState);
            }
            else if (state == shootState && shootState.isComplete)
            {
                Set(idleState);
                nextShotTime = Time.time + shotIntervalInSeconds; // Schedule next shot
            }
        }


    }

    public void GetDistanceFromTarget()
    {
        distanceFromTarget = Vector2.Distance(transform.position, target.transform.position);
    }


    public override void Exit() { }
}
