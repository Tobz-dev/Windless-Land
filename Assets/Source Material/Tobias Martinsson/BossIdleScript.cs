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
    private Quaternion originalRotation;

    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        Debug.Assert(Agent);
        originalRotation = Agent.transform.rotation;
    }

    public override void Enter()
    {
        Agent.GetComponent<BossMechanicsScript>().RespawnPillars();
        CurrentPatrol = Agent.GetPatrolPointByindex(2);
        Agent.NavAgent.SetDestination(CurrentPatrol.position);
        Agent.GetComponent<EnemyHealthScript>().regainHealth(100);
        Agent.GetComponent<BossMechanicsScript>().fallingPlatform1.GetComponent<FallingPlatform>().respawn();
        Agent.GetComponent<BossMechanicsScript>().fallingPlatform2.GetComponent<FallingPlatform>().respawn();
        Agent.GetComponent<BossMechanicsScript>().fallingPlatform3.GetComponent<FallingPlatform>().respawn();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if(go.name != "Boss")
            {
                Destroy(go.gameObject);
            }
        }

    }
    public override void RunUpdate()
    {
        Agent.transform.rotation = originalRotation;
        if (Agent.NavAgent.remainingDistance <= 2f)
        {
            Agent.NavAgent.SetDestination(Agent.gameObject.transform.position);
        }

        if (!Physics.Linecast(Agent.transform.position, Agent.PlayerPosition, Agent.CollisionLayer) && (Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) < chaseDistance))
        {

            StateMachine.ChangeState<BossChooseAttackState>();
        }
    }
}
