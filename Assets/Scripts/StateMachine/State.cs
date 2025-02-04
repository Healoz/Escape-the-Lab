using UnityEditor.Rendering;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set; }
    protected float startTime;
    public float time => Time.time - startTime;

    protected StateMachineCore core;

    // blackboard variables
    protected Rigidbody2D rigidBody => core.rigidBody;
    protected SpriteRenderer spriteRenderer => core.spriteRenderer;
    protected PlayerScript input => core.input;

    public virtual void Enter() { }
    public virtual void Do() { }

    public virtual void FixDo() { }

    public virtual void Exit() { }

    public void SetCore(StateMachineCore _core)
    {
        core = _core;
    }

    public void Initialise()
    {
        isComplete = false;
        startTime = Time.time;
    }
}
