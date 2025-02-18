using System.Collections;
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
    public bool isHit;
    public float hitDuration;
    public Vector2 projectileDirection;
    public State state => machine.state;
    public bool isAlive => currentHealth > 0;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            projectileDirection = GetDirectionOfProjectile(collision);
            TakeHealth(collision);
            StartCoroutine(SetIsHitStateForHitDuration());
        }
    }

    private Vector2 GetDirectionOfProjectile(Collision2D collision)
    {
        ProjectileScript projectileScript = collision.gameObject.GetComponent<ProjectileScript>(); // gets current colliding projectile
        return (projectileScript.transform.position - transform.position).normalized;
    }

    public void TakeHealth(Collision2D collision)
    {
        Debug.Log("collised with projectile");

        ProjectileScript projectile = collision.gameObject.GetComponent<ProjectileScript>();

        if (projectile == null)
        {
            return;
        }

        currentHealth -= projectile.damageAmount;
    }

    public IEnumerator SetIsHitStateForHitDuration()
    {
        isHit = true;
        yield return new WaitForSeconds(hitDuration);
        isHit = false;
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
