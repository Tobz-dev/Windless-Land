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
    private float aggroDistance;
    private Transform CurrentPatrol;
    private int enrageCounter = 0;

    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        Debug.Assert(Agent);
        originalTime = attackCooldown;
        originalRotation = Agent.transform.rotation;
        aggroDistance = Agent.GetComponent<BossMechanicsScript>().aggroRange;
    }

    public override void Enter()
    {
        Debug.Log("Choosing attack.");

        Agent.transform.rotation = originalRotation;
    }
    public override void RunUpdate()
    {
        Agent.transform.rotation = originalRotation;
        CurrentPatrol = Agent.GetPatrolPointByindex(0);
        Agent.NavAgent.SetDestination(CurrentPatrol.position);

        Debug.Log(Agent.GetComponent<HealthScript>().health);
        Debug.Log(Agent.GetComponent<HealthScript>().health * 0.75);
        if (Agent.GetComponent<HealthScript>().health < Agent.GetComponent<HealthScript>().Maxhealth * 0.75 && enrageCounter == 0)
        {
            enrageCounter++;
            Agent.GetComponent<BossMechanicsScript>().DestroyRandomPillar();
            StateMachine.ChangeState<BossShootingState>();
        }
        else if (Agent.GetComponent<HealthScript>().health < Agent.GetComponent<HealthScript>().Maxhealth * 0.5 && enrageCounter == 1)
        {
            enrageCounter++;
            StateMachine.ChangeState<BossShootingState>();
            Agent.GetComponent<BossMechanicsScript>().DestroyRandomPillar();
        }
        else if(Agent.GetComponent<HealthScript>().health < Agent.GetComponent<HealthScript>().Maxhealth * 0.25 && enrageCounter == 2)
        {
            enrageCounter++;
            StateMachine.ChangeState<BossShootingState>();
            Agent.GetComponent<BossMechanicsScript>().DestroyRandomPillar();
        }

        attackCooldown -= Time.deltaTime;
        if (Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) <= aggroDistance)
        {
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
                        StateMachine.ChangeState<BossFloorAttackState>();
                        break;
                    default:
                        break;
                }
                //StateMachine.ChangeState<randomAttackState>();
                attackCooldown = originalTime;
            }
        }
        else
        {
            StateMachine.ChangeState<BossIdleScript>();

        }
    }
}
