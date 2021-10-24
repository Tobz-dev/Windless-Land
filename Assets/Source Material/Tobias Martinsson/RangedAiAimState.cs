using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tobias Martinsson
[CreateAssetMenu()]
public class RangedAiAimState : State
{
    SomeAgent Agent;
    public float arrowSpeed;
    public float aggroDistance;
    private float timeLeft = 1;
    private float originalTime;

    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        Debug.Assert(Agent);
        originalTime = timeLeft;
    }

    public override void Enter()
    {
        Debug.Log("Entered RangedAiAimState");
    }

    public override void RunUpdate()
    {

        if (Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) <= aggroDistance)
        {
            Agent.NavAgent.updateRotation = false;
            Vector3 targetPostition = new Vector3(Agent.Player.position.x,
                                        Agent.transform.position.y,
                                        Agent.Player.position.z);
            Agent.transform.LookAt(targetPostition);
            Vector3.RotateTowards(Agent.transform.position, Agent.PlayerPosition, 2,0);

            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                Agent.GetComponent<ArrowScript>().shootArrow();
                timeLeft = originalTime;
            }

        }
        else
        {
            StateMachine.ChangeState<RangedAiPatrol>();
        }
    }
}
