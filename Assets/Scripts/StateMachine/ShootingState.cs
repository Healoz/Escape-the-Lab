using UnityEngine;

public class ShootingState : State
{
    public BackAwayState backAwayState;
    public IdleState idleState;
    public ShootState shootState;
    public ShootIntervalScript shootIntervalScript;
    // public float shotIntervalInSeconds;
    public GameObject target;
    public float distanceFromTarget;
    public float distanceToBackAway;
    // public float nextShotTime;

    public override void Enter()
    {
        spriteRenderer.color = Color.cyan;
        Set(idleState, true);
        shootIntervalScript.isShooting = true;
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
        if (shootIntervalScript.currentShotTime >= shootIntervalScript.gracePeriodInterval)
        {
            if (state == idleState)
            {
                Set(shootState);
            }
            else if (state == shootState && shootState.isComplete)
            {
                Set(idleState);
            }
        }
    }

    public void GetDistanceFromTarget()
    {
        distanceFromTarget = Vector2.Distance(transform.position, target.transform.position);
    }

    public override void Exit()
    {
        shootIntervalScript.isShooting = false;
    }
}
