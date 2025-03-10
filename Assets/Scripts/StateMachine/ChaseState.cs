using UnityEngine;

public class ChaseState : State
{
    public GameObject threat;
    public float runForce;
    public EdgeChecker edgeChecker;

    public override void Enter()
    {



    }

    public override void Do()
    {
        // chase logic, always happening
        float direction = GetChaseDirection();

        if (direction == -1) // if player in left direction
        {
            if (edgeChecker.isLeftLedge)
            {
                rigidBody.linearVelocity = new Vector2(0, rigidBody.linearVelocity.y); // enemy stops moving
                return; // don't move if enemy is in direction of ledge
            }
        }
        else if (direction == 1) // if player in right direction
        {
            if (edgeChecker.isRightLedge)
            {
                rigidBody.linearVelocity = new Vector2(0, rigidBody.linearVelocity.y); // enemy stops moving
                return;
            }
        }

        rigidBody.linearVelocity = new Vector2(runForce * direction, rigidBody.linearVelocity.y);

    }

    public float GetChaseDirection()
    {
        Vector2 threatPosition = threat.transform.position;

        if (threatPosition.x > transform.position.x)
        {
            // if threat to right, move right
            return 1;
        }
        else
        {
            return -1;
        }

    }

    public override void Exit()
    {
        rigidBody.linearVelocity = new Vector2(0, rigidBody.linearVelocity.y);
    }
}
