using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tim Ag�lii
//kod tagen fr�n AIChase
[CreateAssetMenu()]
public class HeavyChase : State
{
    SomeAgent Agent;
    public float Speed;
    public float AttackDistance;

    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        Debug.Assert(Agent);
    }

    public override void Enter()
    {
        Agent.NavAgent.speed = Speed;
    }

    public override void RunUpdate()
    {
        Agent.NavAgent.SetDestination(Agent.PlayerPosition);

        if (Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) <= AttackDistance)
        {
            Debug.Log("Entering attack pattern");
            StateMachine.ChangeState<HeavyAttackpattern>();
        }

        if (Physics.Linecast(Agent.transform.position, Agent.PlayerPosition, Agent.CollisionLayer))
        {
            Debug.Log("Entering patrol");
            StateMachine.ChangeState<HeavyPatrol>();
        }



    }
}
