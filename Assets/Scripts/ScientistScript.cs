using UnityEngine;

public class ScientistScript : StateMachineCore
{
    [Header("State References")]
    public ShootingState shootingState;
    public ChaseState chaseState;
    public IdleState idleState;
    public DeadState deadState;

    [Header("Scientist Specific Variables")]
    public DetectionRadiusScript detectionRadiusScript;
    public bool playerDetected => detectionRadiusScript.playerCurrentlyDetected;
    public bool playerHasBeenDetected => detectionRadiusScript.playerHasBeenDetected;


    void Start()
    {
        SetupInstances();
        machine.Set(idleState);

        // ignore projectile collisions
        Physics2D.IgnoreLayerCollision(8, 7, true);
    }

    // Update is called once per frame
    void Update()
    {
        SelectState();
    }

    void FixedUpdate()
    {
        FixedSelectState();
    }

    public void SelectState()
    {
        if (!isAlive) // do nothing else if is dead
        {
            machine.Set(deadState);
        }
        else
        {
            if (playerDetected) // only shoot if player in range
            {
                machine.Set(shootingState);
            }
            else if (playerHasBeenDetected) // player out of range, but has been seen before
            {
                machine.Set(chaseState);
            }
            else
            { // has never seen player, idle (TODO: patrol state)
                machine.Set(idleState);
            }
        }

        state.DoBranch();

    }

    public void FixedSelectState()
    {
        state.FixedDoBranch();
    }

}
