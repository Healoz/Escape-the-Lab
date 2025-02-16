using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerScript : StateMachineCore
{
    [Header("State References")]
    // State references
    public AirState airState;
    public IdleState idleState;
    public RunState runState;
    public EvadeState evadeState;
    public ForcePushState forcePushState;
    public DeadState deadState;

    [Header("Player Specific Variables")]

    // input
    public float xInput;
    public float yInput;

    public ForceDirectionScript forceDirectionScript;
    public GameObject projectileReference;


    void Start()
    {
        SetupInstances();
        machine.Set(idleState);

        InitialiseValues();


    }

    // Update is called once per frame
    void Update()
    {

        GetInputs();

        CheckInput();

        SelectState();

        // ignore projectile collisions
        Physics2D.IgnoreLayerCollision(3, 6, true);

        machine.state.Do();

    }

    public void InitialiseValues()
    {
        currentHealth = maxHealth;
    }

    void GetInputs()
    {
        // getting button presses
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

    }

    public void CheckInput()
    {
        if (!isAlive)
        {
            return; // don't recieve input if player is dead
        }

        if (Input.GetKeyDown(KeyCode.Space) && groundColliderScript.isGrounded)
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
        if (Input.GetMouseButtonDown(1) && state != evadeState && evadeState.evadeLogic.evadeCooldownTime >= evadeState.evadeLogic.evadeMaxCooldownTime)
        {
            ForceEvade();
        }

        // Force push on left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            ForcePush();
        }

    }

    public void Jump()
    {
        // isGrounded = false;
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

        evadeState.evadeLogic.isEvading = true;

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

        if (!isAlive) // do nothing else if player is dead
        {
            machine.Set(deadState);
            StartCoroutine(RestartLevel()); // restart level on death after short delay
        }
        else
        {
            // evading trumps every other state regardless of movement (you can still mvoe while evading)
            if (evadeState.evadeLogic.isEvading)
            {
                machine.Set(evadeState);
            }
            else if (groundColliderScript.isGrounded)
            {
                if (xInput != 0)
                {
                    machine.Set(runState);

                }
                else
                {
                    machine.Set(idleState);

                }
            }
            else
            {
                machine.Set(airState);
            }
        }

    }

    public IEnumerator RestartLevel()
    {
        float restartDelay = 1.3f;

        yield return new WaitForSeconds(restartDelay);

        // reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
