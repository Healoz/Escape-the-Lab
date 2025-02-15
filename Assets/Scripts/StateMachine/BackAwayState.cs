using UnityEngine;

public class BackAwayState : State
{
    public GameObject threat;
    public float backAwayForce;
    public float backAwayShotInterval;
    public ShootState shootState;
    public float nextShotTime;

    public override void Enter()
    {
        spriteRenderer.color = Color.blue;
        nextShotTime = Time.time;

    }
    public override void Do()
    {
        // back away logic - always happening
        float direction = GetBackAwayDirection();

        rigidBody.linearVelocity = new Vector2(backAwayForce * direction, rigidBody.linearVelocity.y);


        // shooting logic
        // Only shoot if enough time has passed since last shot
        if (state != shootState && Time.time >= nextShotTime)
        {
            Set(shootState);
        }
        else if (state == shootState)
        {
            if (shootState.isComplete)
            {
                Set(null);
                nextShotTime = Time.time + backAwayShotInterval;
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
    }
}
