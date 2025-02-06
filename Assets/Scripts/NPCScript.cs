using UnityEngine;

public class NPCScript : StateMachineCore
{
    public PatrolState patrolState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupInstances();
        machine.Set(patrolState);

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
