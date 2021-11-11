using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tobias Martinsson
[CreateAssetMenu()]
public class BossIdleScript : State
{
    public float chaseDistance;
    SomeAgent Agent;

    private Transform CurrentPatrol;

    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        Debug.Assert(Agent);
    }

    public override void Enter()
    {
        Agent.GetComponent<HealthScript>().regainHealth(100);
    }
    public override void RunUpdate()
    {

        if (!Physics.Linecast(Agent.transform.position, Agent.PlayerPosition, Agent.CollisionLayer) && (Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) < chaseDistance))
        {

            StateMachine.ChangeState<BossChooseAttackState>();
        }
    }
}
