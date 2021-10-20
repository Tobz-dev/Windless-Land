using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tobias Martinsson
[CreateAssetMenu()]
public class AiChasePlayer : State
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
      
            //StateMachine.ChangeState<AiAttack>();
        }   

        if (Physics.Linecast(Agent.transform.position, Agent.PlayerPosition, Agent.CollisionLayer))
        {
        
            StateMachine.ChangeState<AiPatrolState>();
        }

        

    }
}
