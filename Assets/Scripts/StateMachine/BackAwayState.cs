using UnityEngine;

public class BackAwayState : State
{
    public GameObject threat;
    public float backAwayForce;
    public ShootIntervalScript shootIntervalScript;
    public ShootState shootState;
    public RetreatState retreatState;

    public override void Enter()
    {
        spriteRenderer.color = Color.blue;
        shootIntervalScript.isShooting = true;

    }
    public override void Do()
    {
        // back away logic - always happening
        float direction = GetBackAwayDirection();

        rigidBody.linearVelocity = new Vector2(backAwayForce * direction, rigidBody.linearVelocity.y);


        // shooting logic
        // Only shoot if enough time has passed since last shot
        if (state != shootState && shootIntervalScript.currentShotTime >= shootIntervalScript.gracePeriodInterval)
        {
            Set(shootState);
        }
        else if (state == shootState)
        {
            if (shootState.isComplete)
            {
                Set(retreatState);
            }
        }

    }

    public float GetBackAwayDirection()
    {
        Vector2 threatPosition = threat.transform.position;

        if (threatPosition.x > transform.position.x)
        {
            // if threat to right, move left
            return -1;
        }
        else
        {
            return 1;
        }

    }

    public override void Exit()
    {
        rigidBody.linearVelocity = new Vector2(0, rigidBody.linearVelocity.y);
        shootIntervalScript.isShooting = false;
    }
}
