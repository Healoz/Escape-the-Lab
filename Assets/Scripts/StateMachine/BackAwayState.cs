using UnityEngine;

public class BackAwayState : State
{
    public GameObject threat;
    public float backAwayForce;
    public EdgeChecker edgeChecker;
    public ShootIntervalScript shootIntervalScript;
    public ShootState shootState;
    public RetreatState retreatState;

    public override void Enter()
    {

        shootIntervalScript.isShooting = true;

    }
    public override void Do()
    {
        HandleMovement();
        HandleShooting();

    }

    private void HandleMovement()
    {
        float direction = GetBackAwayDirection();
        bool atLedge = (direction == -1 && edgeChecker.isLeftLedge) ||
                       (direction == 1 && edgeChecker.isRightLedge);

        if (atLedge)
        {
            // Stop moving if at ledge in direction of movement
            rigidBody.linearVelocity = new Vector2(0, rigidBody.linearVelocity.y);
        }
        else
        {
            // Move away from target
            rigidBody.linearVelocity = new Vector2(backAwayForce * direction, rigidBody.linearVelocity.y);
        }
    }

    private void HandleShooting()
    {
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
