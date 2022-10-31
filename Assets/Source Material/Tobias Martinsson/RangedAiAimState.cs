using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tobias Martinsson
[CreateAssetMenu()]
public class RangedAiAimState : State
{
    SomeAgent Agent;
    public float aggroDistance;
    public float shootCooldown;
    private float originalTime;
    private Transform CurrentPatrol;

    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        Debug.Assert(Agent);
        originalTime = shootCooldown;
    }

    public override void Enter()
    {
        Debug.Log("Entered RangedAiAimState");
        CurrentPatrol = Agent.transform;
        Agent.NavAgent.SetDestination(CurrentPatrol.position);
        Agent.animator.SetTrigger("DrawBow");
        //Agent.transform.rotation = lookRotation;
    }

    public override void RunUpdate()
    {

        if (Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) <= aggroDistance)
        {
            
            Agent.NavAgent.updateRotation = false;
            Vector3 targetPostition = new Vector3(Agent.Player.position.x,
                                        Agent.transform.position.y,
                                        Agent.Player.position.z);
            Agent.transform.LookAt(targetPostition);
            Vector3.RotateTowards(Agent.transform.position, Agent.PlayerPosition, 2,0);
            Agent.animator.SetTrigger("DrawBow");

            shootCooldown -= Time.deltaTime;
            if (shootCooldown < 0)
            {
                Debug.Log("Entered RunUpdate, firing");
                Agent.animator.SetTrigger("BowRecoil");
                //Agent.GetComponent<ArrowScript>().shootArrow();
                shootCooldown = originalTime;
                Agent.animator.SetTrigger("BowStop");
            }

        }
        else
        {
            StateMachine.ChangeState<RangedAiPatrol>();
        }
    }
}
