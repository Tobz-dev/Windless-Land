using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tobias Martinsson
[CreateAssetMenu()]
public class BossFloorAttackState : State
{
    SomeAgent Agent;
    private float originalTime;
    private Quaternion originalRotation;

    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        Debug.Assert(Agent);
        originalRotation = Agent.transform.rotation;
    }

    public override void Enter()
    {
        Agent.transform.rotation = originalRotation;
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
