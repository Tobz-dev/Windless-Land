using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tim Agélii
// statemachine kod tagen från aipatrol
[CreateAssetMenu()]
public class LightPatrol : State
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
        Agent.animator.SetFloat("XSpeed", 0);
        Agent.animator.SetFloat("YSpeed", 1);
    }
    public override void RunUpdate()
    {
        if (Agent.NavAgent.remainingDistance < 2.0f)
        {
            CurrentPatrol = Agent.GetPatrolPoint;
            Agent.NavAgent.SetDestination(CurrentPatrol.position);
        }
        Color color = new Color(0.0f, 0.0f, 1.0f);
        Debug.DrawLine(Agent.transform.position, Agent.PlayerPosition, color);
        if (!Physics.Linecast(Agent.transform.position, Agent.PlayerPosition, Agent.CollisionLayer) && (Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) < chaseDistance))
        {
            Debug.Log("Chasing State");
            StateMachine.ChangeState<LightChase>();
        }
    }
}
