using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tobias Martinsson
[CreateAssetMenu()]
public class BossFloorAttackState : State
{
    SomeAgent Agent;
    private float originalTime;
    public float aggroDistance;

    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        aggroDistance = Agent.GetComponent<BossMechanicsScript>().aggroRange;
        Debug.Assert(Agent);
    }

    public override void Enter()
    {

        //Debug.Log("Attack #1. Floor attack.");
        int rand = randomNumber();
        Debug.Log(rand);
        if (rand == 0)
        {
            Agent.GetComponent<BossMechanicsScript>().FadeIn(Agent.GetComponent<BossMechanicsScript>().leftFloor);
        }
        else if(rand == 1)
        {
            Agent.GetComponent<BossMechanicsScript>().FadeIn(Agent.GetComponent<BossMechanicsScript>().rightFloor);
        }
    }
    public override void RunUpdate()
    {

        StateMachine.ChangeState<BossChooseAttackState>();
    }

    public int randomNumber()
    {
        return Random.Range(0, 2);
    }
}
