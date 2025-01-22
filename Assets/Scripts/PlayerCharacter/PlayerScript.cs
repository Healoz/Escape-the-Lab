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

    // input
    public float xInput;
    public float yInput;

    public Vector3 mousePosition;
    public GameObject mouseReticle;
    public GameObject forceDirection;

    public float rotationSpeed = 1f;
    public Transform rotateAround;


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
        if (Input.GetKey(KeyCode.R))
        {
            forceDirection.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            forceDirection.transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.T))
        {
            forceDirection.transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }

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

        GetForceDirection();

    }

    void GetForceDirection()
    {
        // getting mouse position
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // z value should be 0, not -10
        mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);
        mouseReticle.transform.position = mousePosition;

        forceDirection.transform.position = transform.position;
    }

    public void CheckInput()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            rigidBody.linearVelocityY = airState.jumpStrength;
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
