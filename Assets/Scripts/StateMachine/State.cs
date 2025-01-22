using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set; }
    protected float startTime;
    public float time => Time.time - startTime;

    protected Rigidbody2D rigidBody;

    // blackboard variables
    protected SpriteRenderer spriteRenderer;
    protected PlayerScript input;

    public virtual void Enter() { }
    public virtual void Do() { }

    public virtual void FixDo() { }

    public virtual void Exit() { }

    public void Setup(Rigidbody2D _rigidBody, SpriteRenderer _spriteRenderer, PlayerScript _playerScript)
    {
        rigidBody = _rigidBody;
        spriteRenderer = _spriteRenderer;
        input = _playerScript;
    }

    public void Initialise()
    {
        isComplete = false;
        startTime = Time.time;
    }
}
