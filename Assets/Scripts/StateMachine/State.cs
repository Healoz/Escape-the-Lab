using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set; }
    protected float startTime;
    public float time => Time.time - startTime;

    protected Rigidbody2D rigidBody;
    protected bool isGrounded;
    protected SpriteRenderer spriteRenderer;
    public virtual void Enter() { }
    public virtual void Do() { }

    public virtual void FixDo() { }

    public virtual void Exit() { }
}
