using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Main Author: Tim Agélii
//statemachine kod tagen från aiattack
[CreateAssetMenu()]
public class LightAttackpattern : State
{
    SomeAgent Agent;


    public Material attackIndicatorMaterial;
    public Material startMaterial;
    private bool stopAttack = false;

    private float playerToEnemyDistance;

   
    [SerializeField]
    private float enemyStoppingDistance;
    [SerializeField]
    private float enemyMeleeDistance;
    [SerializeField]
    private float outOfRange;

    bool inMeleeRange = false;

    private float attackTimer = 0;

    [SerializeField]
    private float turnSpeed;

    private float moveSpeed = 0;

 

    Vector3 turnDirection;

    //public GameObject hitBox;

    private bool allowStop = true;


  
    private bool startAttack = false;


    private bool waitToAttack = true;

    private bool inAttack = false;

    

    //hitbox variables



    [SerializeField]
    private float swingCooldownTime;


    [SerializeField]
    private float attackWaitTime;


    [SerializeField]
    private Animator enemyAnim;

    

    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        Debug.Assert(Agent);




    }

    public void Awake()
    {
      


    }
    public override void RunUpdate()
    {
      
           
         moveSpeed = getSpeed();
        Agent.NavAgent.speed = moveSpeed;
        Agent.NavAgent.SetDestination(Agent.PlayerPosition);

        stopAttack = getStopAttack();


        playerToEnemyDistance = (Agent.PlayerPosition - Agent.NavAgent.transform.position).magnitude;

      
        if (Mathf.Abs(playerToEnemyDistance) <= enemyStoppingDistance)
        {
            Agent.NavAgent.isStopped = true;
        }
        else {
            Agent.NavAgent.isStopped = false;
        }

        if ((Mathf.Abs(playerToEnemyDistance) <= enemyMeleeDistance) && inAttack == false)
        {
            inMeleeRange = true;
        }
        else {

            inMeleeRange = false;
                }
        



            AttackPattern();
        
        if ((Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) >= outOfRange) && allowStop)
        {

            StateMachine.ChangeState<LightChase>();

        }
    }

    float getSpeed(){
        return Agent.transform.GetComponentInParent<LightAttackEvent>().GetEnemySpeed();
    }

    bool getStopAttack() {
        return Agent.transform.GetComponentInParent<LightAttackEvent>().GetStopAttack();
    }
    void SetStopAttackFalse()
    {
         Agent.transform.GetComponentInParent<LightAttackEvent>().SetStopAttack(false);
    }

    void AttackPattern()
    {
        inAttack = true;

        LookAtPlayer();
        if (waitToAttack == true)
        {
      
            WaitToAttack();
           
        }
        if (startAttack == true)
        {
            Attack();
         
        }
      
     
        if (stopAttack == true) {
            ResetPattern();
            inAttack = false;
        }


    }

    void WaitToAttack()
    {
        allowStop = false;
        if (AttackWaitTimer(attackWaitTime))
        {
            waitToAttack = false;
         
            startAttack = true;

        }
     

    }

    void LookAtPlayer()
    {
        if (Agent.NavAgent.isStopped == true) {
            turnDirection = Agent.Player.position - Agent.transform.position;
            turnDirection.Normalize();
            Agent.transform.rotation = Quaternion.Slerp(Agent.transform.rotation, Quaternion.LookRotation(turnDirection), turnSpeed * Time.deltaTime);
        }
        

        

    }

    void Attack()
    {


        if (startAttack == true)
        {

            if (inMeleeRange)
            {
                Agent.animator.SetTrigger("StartAttack");
            }
            else {
              

                Agent.animator.SetTrigger("StartAttack");

            }

            startAttack = false;



        }
    }
   

    

    void ResetPattern() {
        allowStop = true;
        waitToAttack = true;
       
        SetStopAttackFalse();
        Agent.animator.SetTrigger("StopAttack");
    }

   
   


    private bool AttackWaitTimer(float seconds)
    {

        attackTimer += Time.deltaTime;

        if (attackTimer >= seconds)
        {

            attackTimer = 0;
            return true;


        }
        return false;
    }

}

