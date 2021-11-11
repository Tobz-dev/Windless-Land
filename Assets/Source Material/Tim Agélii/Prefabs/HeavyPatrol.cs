using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tim Agélii
// statemachine kod tagen från aipatrol
[CreateAssetMenu()]
public class HeavyPatrol : State
{
    public float chaseDistance;
    public float Speed;
    SomeAgent Agent;

    private Transform CurrentPatrol;

    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        Debug.Assert(Agent);
    }

    public override void Enter()
    {
        CurrentPatrol = Agent.GetPatrolPoint;
        Agent.NavAgent.SetDestination(CurrentPatrol.position);
        Agent.NavAgent.speed = Speed;
    }
    public override void RunUpdate()
    {
        if (Agent.NavAgent.remainingDistance < 2.0f)
        {
            if(PrototypeScript.allowedMove == true)
            {
                CurrentPatrol = Agent.GetPatrolPoint;
                Agent.NavAgent.SetDestination(CurrentPatrol.position);
                Agent.animator.SetFloat("XSpeed", 0);
                Agent.animator.SetFloat("YSpeed", 1);
            }
            else
            {
                Agent.NavAgent.SetDestination(Agent.transform.position);
            }

        }

        if (!Physics.Linecast(Agent.transform.position, Agent.PlayerPosition, Agent.CollisionLayer) && (Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) < chaseDistance))
        {

            StateMachine.ChangeState<HeavyChase>();
        }
    }
}
