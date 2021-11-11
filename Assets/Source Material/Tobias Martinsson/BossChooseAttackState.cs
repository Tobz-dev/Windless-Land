using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tobias Martinsson
[CreateAssetMenu()]
public class BossChooseAttackState : State
{
    SomeAgent Agent;
    public float attackCooldown;
    private float originalTime;
    private Quaternion originalRotation;

    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        Debug.Assert(Agent);
        originalTime = attackCooldown;
        originalRotation = Agent.transform.rotation;
    }

    public override void Enter()
    {
        Debug.Log("Choosing attack.");
        Agent.transform.rotation = originalRotation;
    }
    public override void RunUpdate()
    {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown < 0)
        {
            int randomAttackStateIndex = Random.Range(2, Agent.States.Length);
            //Debug.Log(randomAttackStateIndex);
            switch (randomAttackStateIndex)
            {
                case 2:
                    StateMachine.ChangeState<BossFloorAttackState>();
                    break;
                case 3:
                    StateMachine.ChangeState<BossShootingState>();
                    break;
                default:
                    break;
            }
            //StateMachine.ChangeState<randomAttackState>();
            attackCooldown = originalTime;
        }

        //Agent.GetComponent<BossMechanicsScript>().fadeIn(Agent.GetComponent<BossMechanicsScript>().leftFloor);
        //Shoots arrows continuously at player.
        /*
        Agent.NavAgent.updateRotation = false;
        Vector3 targetPostition = new Vector3(Agent.Player.position.x,
                                    Agent.transform.position.y,
                                    Agent.Player.position.z);
        Agent.transform.LookAt(targetPostition);
        Vector3.RotateTowards(Agent.transform.position, Agent.PlayerPosition, 2, 0);

        shootCooldown -= Time.deltaTime;
        if (shootCooldown < 0)
        {
            Agent.GetComponent<ArrowScript>().shootArrow();
            shootCooldown = originalTime;
        }
        */



    }
}
