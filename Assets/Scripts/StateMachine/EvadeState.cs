using UnityEngine;

public class EvadeState : State
{
    public float evadeTime;
    public float evadeStrength;
    public Vector2 direction;
    public override void Enter()
    {
        spriteRenderer.color = Color.magenta;
    }
    public override void Do()
    {
        //  end condition
        if (time > evadeTime)
        {
            Debug.Log("evade done");
            input.isEvading = false; // only this script knows the state time elapsed
            isComplete = true;
        }

    }
    public override void Exit() { }

}
