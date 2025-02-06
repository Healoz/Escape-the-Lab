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
    protected GroundColliderScript groundColliderScript => core.groundColliderScript;
    protected float currentHealth => core.currentHealth;
    protected float maxHealth => core.maxHealth;

    // Hierarchal state machine
    public StateMachine machine;
    protected StateMachine parent;
    public State state => machine.state;


    // protected PlayerScript input => core.input;

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }
    public void DoBranch()
    {
        Do();
        state?.DoBranch();
    }

    public void FixedDoBranch()
    {
        FixedDo();
        state?.FixedDoBranch();
    }

    public void Set(State newState, bool forceReset = false)
    {
        machine.Set(newState, forceReset);
    }

    public void SetCore(StateMachineCore _core)
    {
        machine = new StateMachine();
        core = _core;
    }

    public void Initialise(StateMachine _parent)
    {
        parent = _parent;
        isComplete = false;
        startTime = Time.time;
    }
}
