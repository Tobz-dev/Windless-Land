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

    [SerializeField]
    private float outOfRange;


    private float attackTimer = 0;

    [SerializeField]
    private float turnSpeed;

    private float moveSpeed = 0;

    private float stoppingDistance = 2f;

    Vector3 turnDirection;

    //public GameObject hitBox;

    private bool allowStop = true;


    private bool startReset = false;
    private bool startAttack = false;


    private bool waitToAttack = true;

    

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
        Agent.NavAgent.SetDestination(Agent.PlayerPosition);

        stopAttack = getStopAttack();
    
      

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
        if (waitToAttack == true)
        {
         //   Agent.NavAgent.isStopped = true;
            WaitToAttack();
        //   LookAtPlayer();
        }
        if (startAttack == true)
        {
            Attack();
       //    LookAtPlayer();
        }
      
     
        if (stopAttack == true) {
            ResetPattern();
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
       
        
            turnDirection = Agent.Player.position - Agent.transform.position;
            turnDirection.Normalize();
            Agent.transform.rotation = Quaternion.Slerp(Agent.transform.rotation, Quaternion.LookRotation(turnDirection), turnSpeed * Time.deltaTime);
        

    }

    void Attack()
    {
        if (startAttack == true)
        {

          
           
                startAttack = false;
              
                Agent.animator.SetTrigger("StartAttack");


            

        }
    }
   

    

    void ResetPattern() {
        allowStop = true;
        waitToAttack = true;
        //Agent.NavAgent.isStopped = false;
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

