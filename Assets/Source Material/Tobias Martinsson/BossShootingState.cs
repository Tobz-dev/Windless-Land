using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tobias Martinsson
[CreateAssetMenu()]
public class BossShootingState : State
{
    SomeAgent Agent;
    public float attackCooldown;
    private float originalTime;
    public float arrowAmount;
    private float totalArrows;
    private float aggroDistance;
    public GameObject addEnemy;

    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        Debug.Assert(Agent);
        originalTime = attackCooldown;
        aggroDistance = Agent.GetComponent<BossMechanicsScript>().aggroRange;
    }

    public override void Enter()
    {
        totalArrows = arrowAmount;
        GameObject add = Instantiate(addEnemy);
        add.transform.position = Agent.GetComponent<BossMechanicsScript>().adSpawnPoint1.transform.position;
        GameObject add2 = Instantiate(addEnemy);
        add2.transform.position = Agent.GetComponent<BossMechanicsScript>().adSpawnPoint2.transform.position;
    }

    public override void RunUpdate()
    {
        if (Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) <= aggroDistance)
        {
            

            if (totalArrows > 0)
            {
                Agent.NavAgent.updateRotation = false;
                Vector3 targetPostition = new Vector3(Agent.Player.position.x,
                                            Agent.transform.position.y,
                                            Agent.Player.position.z);
                Agent.transform.LookAt(targetPostition);
                Vector3.RotateTowards(Agent.transform.position, Agent.PlayerPosition, 2, 0);

                attackCooldown -= Time.deltaTime;
                if (attackCooldown < 0)
                {
                    Agent.GetComponent<ArrowScript>().shootArrow();
                    totalArrows--;
                    attackCooldown = originalTime;
                }
            }
            else if (totalArrows == 0)
            {
                StateMachine.ChangeState<BossChooseAttackState>();
            }
        }
        else
        {
            StateMachine.ChangeState<BossIdleScript>();
        }
        
    }

}
