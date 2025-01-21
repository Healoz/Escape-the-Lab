using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // State references
    public State airState;
    public State idleState;
    public State runState;

    public State state;

    public SpriteRenderer spriteRenderer;

    public Rigidbody2D rigidBody;
    public float jumpStrength;
    public float runStrength;
    public bool isGrounded = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        CheckInput();

        if (state.isComplete)
        {
            SelectState();
        }

        state.Do();

    }

    public void CheckInput()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            rigidBody.linearVelocityY = jumpStrength;

            // UpdateState();
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveHorizontal(true);
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            rigidBody.linearVelocityX = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            MoveHorizontal(false);
        }
    }

    public void MoveHorizontal(bool isGoingRight)
    {
        float runForceValue = runStrength;
        if (!isGoingRight)
        {
            // make negative if going left
            runForceValue = runForceValue * -1;
        }

        rigidBody.linearVelocityX = runForceValue;
    }

    public void SelectState()
    {

        if (isGrounded)
        {
            if (rigidBody.linearVelocityX != 0)
            {
                state = runState;

            }
            else
            {
                state = idleState;

            }
        }
        else
        {
            state = airState;
        }

        state.Enter();
    }

}
