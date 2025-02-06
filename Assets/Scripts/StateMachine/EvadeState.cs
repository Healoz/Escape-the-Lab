using UnityEngine;

public class EvadeState : State
{
    public float evadeTime;
    public float evadeStrength;
    public Vector2 direction;
    public float evadeMaxCooldownTime;
    public override void Enter()
    {
        spriteRenderer.color = Color.magenta;
        // input.evadeCooldownTime = 0f; // resets timer to 0

    }
    public override void Do()
    {

        Vector2 currentVelocity = rigidBody.linearVelocity;
        rigidBody.linearVelocity = new Vector2(
            currentVelocity.x + (-1 * direction.x) * evadeStrength,
            (-1 * direction.y) * evadeStrength // -1 inverts the direction
        );


        //  end condition
        if (time > evadeTime)
        {
            Debug.Log("evade done");
            // input.isEvading = false; // only this script knows the state time elapsed
            // needs to be fixed, have a way for Player object to know if evade is done
            isComplete = true;
        }

    }
    public override void Exit()
    {

    }

}
