using UnityEngine;

public class ScientistScript : StateMachineCore
{
    public ShootingState shootingState;
    void Start()
    {
        SetupInstances();
        machine.Set(shootingState);
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
