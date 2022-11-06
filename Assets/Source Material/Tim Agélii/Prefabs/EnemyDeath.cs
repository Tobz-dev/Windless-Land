using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Main Author: Lukas Kleberg
// statemachine kod tagen LightPatrol
[CreateAssetMenu()]
public class EnemyDeath : State
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
        CurrentPatrol = Agent.GetPatrolPoint();
        Agent.NavAgent.SetDestination(CurrentPatrol.position);
        Agent.NavAgent.speed = Speed;
        Agent.animator.SetFloat("XSpeed", 0);
        Agent.animator.SetFloat("YSpeed", 0);
        //Debug.Log("entered light death state.");
    }
    public override void RunUpdate()
    {
        //Do nothing.
    }
}
