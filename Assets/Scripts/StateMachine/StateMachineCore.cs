using UnityEngine;

public abstract class StateMachineCore : MonoBehaviour
{
    // blackboard variables
    [Header("Blackboard variables")]
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;
    // public PlayerScript input;
    public GroundColliderScript groundColliderScript;
    public StateMachine machine;
    public float currentHealth;
    public float maxHealth;
    public State state => machine.state;

    public void SetupInstances()
    {
        machine = new StateMachine();

        State[] allChildStates = GetComponentsInChildren<State>();
        foreach (State state in allChildStates)
        {
            state.SetCore(this);
        }
    }
}
