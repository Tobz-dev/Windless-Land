using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tim Agélii
//kod tagen från AIChase
[CreateAssetMenu()]
public class LightChase : State
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
        if(PrototypeScript.allowedMove == true)
        {
            Agent.NavAgent.SetDestination(Agent.PlayerPosition);
        }
        else
        {
            Agent.NavAgent.SetDestination(Agent.transform.position);
        }
        

        if (Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) <= AttackDistance)
        {
            Debug.Log("Entering attack pattern");
            StateMachine.ChangeState<LightAttackpattern>();
        }

        if (Physics.Linecast(Agent.transform.position, Agent.PlayerPosition, Agent.CollisionLayer))
        {
            Debug.Log("Entering patrol");
            StateMachine.ChangeState<LightPatrol>();
        }



    }
}
