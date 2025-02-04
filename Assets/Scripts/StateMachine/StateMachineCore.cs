using UnityEngine;

public abstract class StateMachineCore : MonoBehaviour
{
    // blackboard variables
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;
    public PlayerScript input;
    public StateMachine machine;

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
