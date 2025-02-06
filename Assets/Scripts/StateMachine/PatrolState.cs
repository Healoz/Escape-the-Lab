using UnityEngine;

public class PatrolState : State
{
    public NavigateState navigate;
    public IdleState idle;
    public Transform anchor1;
    public Transform anchor2;

    void GoToNextDestination()
    {
        float randomSpot = Random.Range(anchor1.position.x, anchor2.position.x);
        navigate.destination = new Vector2(randomSpot, core.transform.position.y);
        Set(navigate, true);
    }
    public override void Enter()
    {
        GoToNextDestination();
    }

    public override void Do()
    {
        if (state == navigate) //idle
        {
            if (navigate.isComplete)
            {
                Set(idle, true);
                rigidBody.linearVelocity = new Vector2(0, rigidBody.linearVelocity.y);
            }
        }
        else
        {
            if (state.time > 1) //navigate
            {
                GoToNextDestination();
            }
        }
    }

    public override void Exit()
    {

    }
}
