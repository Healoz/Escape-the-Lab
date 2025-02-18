using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class RagdollState : State
{
    public float knockBackForce;
    public float upwardForce;

    public Vector2 knockBackDirection;


    public override void Enter()
    {
        rigidBody.linearVelocity = new Vector2(0, rigidBody.linearVelocity.y);
        spriteRenderer.color = Color.blue;

        knockBackDirection = -projectileDirection.normalized; // get opposite direction of projectile
        rigidBody.linearVelocity = Vector2.zero; // reset velocity
        rigidBody.freezeRotation = false; // unfreeze rotation

    }
    public override void Do()
    {
        // end condition here
        if (!isHit) // isHit timer complete
        {
            isComplete = true;
            return;
        }

        // apply knockback force every frame
        rigidBody.linearVelocity = new Vector2(knockBackDirection.x * knockBackForce, upwardForce);


    }
    public override void Exit()
    {
        spriteRenderer.color = Color.yellow;
        rigidBody.linearVelocity = Vector2.zero;
        rigidBody.rotation = 0f; // Reset to upright position
        rigidBody.angularVelocity = 0f; // Stop any spinning
    }
}
