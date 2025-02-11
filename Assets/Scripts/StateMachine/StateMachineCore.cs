using System.Collections.Generic;
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
    public bool isAlive => currentHealth > 0;

    // blackboard functions
    public void CheckIsDead()
    {
        if (!isAlive)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("collised with projectile");

            ProjectileScript projectile = collision.gameObject.GetComponent<ProjectileScript>();

            if (projectile == null)
            {
                return;
            }

            currentHealth -= projectile.damageAmount;

            CheckIsDead();
        }
    }

    // state machine functions
    public void SetupInstances()
    {
        machine = new StateMachine();

        State[] allChildStates = GetComponentsInChildren<State>();
        foreach (State state in allChildStates)
        {
            state.SetCore(this);
        }
    }


    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Application.isPlaying && state != null)
        {
            List<State> states = machine.GetActiveStateBranch();
            UnityEditor.Handles.Label(transform.position, "Active states: " + string.Join(" > ", states));
        }
#endif
    }
}
