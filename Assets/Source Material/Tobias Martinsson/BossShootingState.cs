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
    public float aggroDistance;
    public GameObject addEnemy;
    private Transform CurrentPatrol;
    public float switchStateCooldown = 2f;
    private bool shotBigArrow = false;

    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        Debug.Assert(Agent);
        originalTime = attackCooldown;

    }

    public override void Enter()
    {
        //Sets all needed variables upon entering this state, deciding how many enemies to spawn based on boss's HP.
        attackCooldown = 2;
        int currentHealth = Agent.GetComponent<EnemyHealthScript>().health;
        int maxHealth = Agent.GetComponent<EnemyHealthScript>().Maxhealth;

        if(currentHealth <= maxHealth * 0.25)
        {
            //SpawnLeftEnemy();
            //SpawnLeftEnemy();
            //SpawnRightEnemy();
            //SpawnRightEnemy();
        }
        else if(currentHealth <= maxHealth * 0.5)
        {
            //SpawnLeftEnemy();
            //SpawnRightEnemy();
            //SpawnRightEnemy();
        }
        else if (currentHealth <= maxHealth * 0.75)
        {
            //SpawnLeftEnemy();
            //SpawnRightEnemy();
        }
        
        totalArrows = arrowAmount;
    }

    public override void RunUpdate()
    {
        //Sets patrolpoint and makes boss walk to it.
        CurrentPatrol = Agent.GetPatrolPointByindex(1);
        Agent.NavAgent.SetDestination(CurrentPatrol.position);
        if (Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) <= aggroDistance) //If player is within aggrodistance (aka not in the spawn-area)
        {
            if (totalArrows > 0) //and the boss has more than 0 arrows left
            {
                //Rotate the boss towards the player, and shoot arrows at a specified interval. 
                Agent.NavAgent.updateRotation = false;
                Vector3 targetPosition = new Vector3(Agent.Player.position.x,
                                            Agent.transform.position.y + 2f,
                                            Agent.Player.position.z);
                //bossHead.transform.LookAt(targetPosition);
                Agent.transform.LookAt(targetPosition);
                Vector3.RotateTowards(Agent.transform.position, Agent.PlayerPosition, 2, 0);

                attackCooldown -= Time.deltaTime;
                if (attackCooldown < 0)
                {
                    Agent.GetComponent<ArrowScript>().shootArrow();
                    totalArrows--;
                    attackCooldown = originalTime;
                }
            }
            else if (totalArrows == 0) //When boss runs out of arrows, shoot a big arrow, destroying a pillar and then return to BossChooseAttackState.
            {
                if (shotBigArrow == false)
                {
                    shotBigArrow = true;


                    Transform pillarTransform = Agent.GetComponent<BossMechanicsScript>().GetRandomPillar();
                    Vector3 pillarPosition = new Vector3(pillarTransform.position.x,
                                               pillarTransform.transform.position.y + 2f,
                                               pillarTransform.position.z);
                    Agent.transform.LookAt(pillarPosition);
                    Vector3.RotateTowards(Agent.transform.position, pillarPosition, 2, 0);

                    Agent.GetComponent<ArrowScript>().shootBigArrow();



                }

                switchStateCooldown -= Time.deltaTime;
                if (switchStateCooldown < 0)
                {
                    shotBigArrow = false;
                    switchStateCooldown = 2f;
                    StateMachine.ChangeState<BossChooseAttackState>();

                }
            }
        }
        else
        {
            StateMachine.ChangeState<BossChooseAttackState>();
        }
    }

    //Spawns the enemies on either side.
    public void SpawnLeftEnemy()
    {
        GameObject add = Instantiate(addEnemy);
        add.transform.position = Agent.GetComponent<BossMechanicsScript>().adSpawnPoint1.transform.position;
    }

    public void SpawnRightEnemy()
    {
        GameObject add = Instantiate(addEnemy);
        add.transform.position = Agent.GetComponent<BossMechanicsScript>().adSpawnPoint2.transform.position;
    }
}
