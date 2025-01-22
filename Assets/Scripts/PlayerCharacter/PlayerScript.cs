using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // State references
    public AirState airState;
    public IdleState idleState;
    public RunState runState;

    public State state;

    public SpriteRenderer spriteRenderer;

    public Rigidbody2D rigidBody;

    public bool isGrounded;

    public float forceEvadeAmount;

    // input
    public float xInput;
    public float yInput;

    public ForceDirectionScript forceDirectionScript;


    void Start()
    {
        idleState.Setup(rigidBody, spriteRenderer, this);
        runState.Setup(rigidBody, spriteRenderer, this);
        airState.Setup(rigidBody, spriteRenderer, this);

        state = idleState;
    }

    // Update is called once per frame
    void Update()
    {

        GetInputs();

        CheckInput();

        SelectState();


        state.Do();

    }

    void GetInputs()
    {
        // getting button presses
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

    }

    public void CheckInput()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();

        }

        if (xInput != 0)
        {
            // moves left or right depending on neg or position xInput
            MoveHorizontal(xInput > 0);
        }
        else
        {
            rigidBody.linearVelocityX = 0;
        }


        if (yInput < 0) // Pressing S or Down arrow
        {
            Scale(60f);
        }
        else
        {
            Scale(120f);
        }

        // Force evade on right mouse click
        if (Input.GetMouseButtonDown(1))
        {
            ForceEvade();
        }

    }

    public void Jump()
    {
        isGrounded = false;
        rigidBody.linearVelocityY = airState.jumpStrength;
    }

    public void Scale(float newScaleAmount)
    {
        Vector3 newScale = transform.localScale;
        newScale.y = newScaleAmount;
        transform.localScale = newScale;
    }

    public void MoveHorizontal(bool isGoingRight)
    {
        float runForceValue = runState.runSpeed;
        if (!isGoingRight)
        {
            // make negative if going left
            runForceValue = runForceValue * -1;
        }

        rigidBody.linearVelocityX = runForceValue;
    }

    public void ForceEvade()
    {
        Debug.Log(forceDirectionScript.direction.normalized);
        rigidBody.AddForce(forceDirectionScript.direction * -forceEvadeAmount, ForceMode2D.Impulse);
    }

    public void SelectState()
    {
        State oldState = state;

        if (isGrounded)
        {
            if (xInput != 0)
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

        if (oldState != state || oldState.isComplete)
        {
            oldState.Exit();
            state.Initialise();
            state.Enter();
        }


    }

}
