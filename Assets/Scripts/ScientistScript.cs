using UnityEngine;

public class ScientistScript : StateMachineCore
{
    public ShootingState shootingState;
    void Start()
    {
        SetupInstances();
        machine.Set(shootingState);

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
        if (state.isComplete)
        {

        }

        state.DoBranch();


    }

    public void FixedSelectState()
    {
        state.FixedDoBranch();
    }
}
