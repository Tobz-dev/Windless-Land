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
        Agent.GetComponent<BossMechanicsScript>().fallingPlatform1.GetComponent<FallingPlatform>().respawn();
        Agent.GetComponent<BossMechanicsScript>().fallingPlatform2.GetComponent<FallingPlatform>().respawn();
        Agent.GetComponent<BossMechanicsScript>().fallingPlatform3.GetComponent<FallingPlatform>().respawn();
    }
    public override void RunUpdate()
    {

        if (!Physics.Linecast(Agent.transform.position, Agent.PlayerPosition, Agent.CollisionLayer) && (Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) < chaseDistance))
        {

            StateMachine.ChangeState<BossChooseAttackState>();
        }
    }
}
