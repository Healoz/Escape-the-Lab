
using UnityEngine;

public class ShootingState : State
{
    [Header("State Variables")]
    public BackAwayState backAwayState;
    public IdleState idleState;
    public ShootState shootState;
    [Header("Shooting Variables")]
    public ShootIntervalScript shootIntervalScript;

    public GameObject target;
    [Header("Distance Variables")]
    public float distanceFromTarget;
    public float distanceToBackAway;
    [Header("Movement Variables")]
    public float currentMoveTime;
    public float moveTime;
    public float[] moveTimeMinAndMax = new float[2];
    public float moveDirection;
    public float runForce;

    public override void Enter()
    {
        spriteRenderer.color = Color.cyan;
        Set(idleState, true);
        shootIntervalScript.isShooting = true;
        moveDirection = Random.value < 0.5f ? 1 : -1; // initialise random direction -1 or 1
        GetNewMoveTime();
    }

    public override void Do()
    {

        GetDistanceFromTarget();

        if (distanceFromTarget < distanceToBackAway) // back away if player too close
        {
            Set(backAwayState);
            return;
        }

        // if not in back away state, create random movement and shoot
        RandomMovementLogic();
        ShootLogic();

    }

    public void RandomMovementLogic()
    {
        if (currentMoveTime >= moveTime) // if time has been reached
        {
            currentMoveTime = 0f;
            moveDirection = moveDirection * -1; // inverts the current move direction
            rigidBody.linearVelocity = new Vector2(0, rigidBody.linearVelocity.y); // reset linear velocity
            GetNewMoveTime();
            return;
        }

        currentMoveTime += Time.deltaTime; // increment the time
        // if time hasnt been reached, keep moving in given direction
        rigidBody.linearVelocity = new Vector2(runForce * moveDirection, rigidBody.linearVelocity.y);
    }

    public void GetNewMoveTime()
    {
        moveTime = Random.Range(moveTimeMinAndMax[0], moveTimeMinAndMax[1]);
    }

    public void ShootLogic()
    {
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
