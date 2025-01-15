using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set; }
    protected float startTime;
    public float time => Time.time - startTime;

    // blackboard variables
    protected Rigidbody2D rigidBody;
    protected SpriteRenderer spriteRenderer;
    protected bool isGrounded;

    public virtual void Enter() { }

    public virtual void Do() { }

    public virtual void FixedDo() { }

    public virtual void Exit() { }



}
