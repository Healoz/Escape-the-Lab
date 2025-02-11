using UnityEngine;

public class ScientistScript : StateMachineCore
{
    [Header("State References")]
    public ShootingState shootingState;
    public IdleState idleState;

    [Header("Scientist Specific Variables")]
    public DetectionRadiusScript detectionRadiusScript;

    public bool playerDetected => detectionRadiusScript.playerDetected;

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
        if (playerDetected) // only shoot if player detected
        {
            machine.Set(shootingState);
        }
        else
        {
            machine.Set(idleState);
        }

        state.DoBranch();


    }

    public void FixedSelectState()
    {
        state.FixedDoBranch();
    }

}
