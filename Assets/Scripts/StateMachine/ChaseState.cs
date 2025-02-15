using UnityEngine;

public class ChaseState : State
{
    public GameObject threat;
    public float runForce;

    public override void Enter()
    {

        spriteRenderer.color = Color.green;

    }
    public override void Do()
    {
        // back away logic - always happening
        float direction = GetChaseDirection();

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
