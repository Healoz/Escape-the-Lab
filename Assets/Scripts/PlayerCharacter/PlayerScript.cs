using UnityEngine;
using UnityEngine.Rendering;

public class PlayerScript : MonoBehaviour
{
    // State references
    public AirState airState;
    public IdleState idleState;
    public RunState runState;
    public EvadeState evadeState;
    public ForcePushState forcePushState;
    public float currentHealth;
    public float maxHealth;

    public State state;

    public SpriteRenderer spriteRenderer;

    public Rigidbody2D rigidBody;

    public bool isGrounded;
    public bool isEvading;
    public float evadeCooldownTime;

    // input
    public float xInput;
    public float yInput;

    public ForceDirectionScript forceDirectionScript;
    public GameObject projectileReference;


    void Start()
    {
        idleState.Setup(rigidBody, spriteRenderer, this);
        runState.Setup(rigidBody, spriteRenderer, this);
        airState.Setup(rigidBody, spriteRenderer, this);
        evadeState.Setup(rigidBody, spriteRenderer, this);

        currentHealth = maxHealth;

        state = idleState;
        evadeCooldownTime = evadeState.evadeMaxCooldownTime;
    }

    // Update is called once per frame
    void Update()
    {

        GetInputs();

        CheckInput();

        SelectState();

        IncrementEvadeCooldown();

        // ignore projectile collisions
        Physics2D.IgnoreLayerCollision(3, 6, true);

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
        if (Input.GetMouseButtonDown(1) && !isEvading && evadeCooldownTime >= evadeState.evadeMaxCooldownTime)
        {
            ForceEvade();
        }

        // Force push on left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            ForcePush();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // taking damage on enemy collision
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject enemy = collision.gameObject;
            EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();

            if (enemyScript == null)
            {
                return;
            }

            currentHealth -= enemyScript.damageAmount;
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

    public void IncrementEvadeCooldown()
    {
        if (evadeCooldownTime < evadeState.evadeMaxCooldownTime)
        {
            evadeCooldownTime += Time.deltaTime; // increment cooldown if cooldowntime is less than the max
        }
    }

    public void ForceEvade()
    {

        isEvading = true;

        evadeState.direction = forceDirectionScript.direction.normalized; // make sure the direction is normalised for consistent speed
    }

    public void ForcePush()
    {
        GameObject projectile = Instantiate(projectileReference, transform.position, Quaternion.identity);
        Rigidbody2D rigidBody = projectile.GetComponent<Rigidbody2D>();

        if (rigidBody == null)
        {
            return;
        }

        rigidBody.AddForce(forceDirectionScript.direction.normalized * forcePushState.pushForce, ForceMode2D.Impulse);
    }

    public void SelectState()
    {
        State oldState = state;

        // evading trumps every other state regardless of movement (you can still mvoe while evading)
        if (isEvading)
        {
            state = evadeState;
        }
        else if (isGrounded)
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
