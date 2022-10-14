using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Main Author: Tobias Martinsson

[CreateAssetMenu()]
public class BossIdleScript : State
{
    //Variables
    public float initialAggroDistance;
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
        //Makes sure when the boss enters 'idle', it resets all relevant variables and disables the hitbox, as well as
        //respawns the platforms at the beginning, as well as destroying any lingering enemies that might be still alive.
        Agent.GetComponent<SphereCollider>().enabled = false;
        GameObject bossCanvas = GameObject.FindGameObjectWithTag("BossHUD");
        bossCanvas.GetComponent<Canvas>().enabled = false;
        Agent.GetComponent<BossMechanicsScript>().RespawnPillars();
        CurrentPatrol = Agent.GetPatrolPointByindex(2);
        Agent.NavAgent.SetDestination(CurrentPatrol.position);
        //Agent.GetComponent<EnemyHealthScript>().regainHealth(100);
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

    //Returns to boss to his idle position, and checks if player comes within aggro distance, to then go into the state where he starts attacking.
    public override void RunUpdate()
    {
        Agent.transform.rotation = originalRotation;
        if (Agent.NavAgent.remainingDistance <= 2f)
        {
            Agent.NavAgent.SetDestination(Agent.gameObject.transform.position);
        }

        if (!Physics.Linecast(Agent.transform.position, Agent.PlayerPosition, Agent.CollisionLayer) && (Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) < initialAggroDistance))
        {

            StateMachine.ChangeState<BossChooseAttackState>();
        }
    }
}
