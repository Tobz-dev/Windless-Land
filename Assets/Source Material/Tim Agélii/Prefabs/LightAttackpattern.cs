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
   
    [SerializeField]
    public float outOfRange;
   
    
    private float attackTimer = 0;

    [SerializeField]
    private float turnSpeed;
  
 
    Vector3 turnDirection;

    //public GameObject hitBox;

    private bool allowStop = true;


    private bool startReset = false;
    private bool startAttack = false;
    private bool startCooldown = false;

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

        AttackPattern();


        if ((Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) >= outOfRange) && allowStop)
        {
            
            StateMachine.ChangeState<LightChase>();
        }
    }

    void AttackPattern()
    {
        if (waitToAttack == true)
        {
            Agent.NavAgent.isStopped = true;
            WaitToAttack();
            LookAtPlayer();
        }
        if (startAttack == true)
        {
            Attack();
            LookAtPlayer();
        }
      
     
        if (startCooldown == true)
        {
            CoolDown();
        }
       
        if (startReset) {
            ResetPattern();
        }



    }

    void WaitToAttack()
    {
        allowStop = false;
        if (AttackWaitTimer(attackWaitTime))
        {
            waitToAttack = false;
            Agent.transform.rotation = Agent.transform.rotation;
            startAttack = true;

        }
        else
        {
            turnDirection = Agent.Player.position - Agent.transform.position;
            turnDirection.Normalize();
            Agent.transform.rotation = Quaternion.Slerp(Agent.transform.rotation, Quaternion.LookRotation(turnDirection), turnSpeed * Time.deltaTime);
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
                startCooldown = true;
                Agent.animator.SetTrigger("StartAttack");


            

        }
    }
    void CoolDown()
    {
        if (startCooldown == true)
        {
            if (AttackWaitTimer(swingCooldownTime))
            {
                ResetPattern();
            
          
            }
        }

    }

    void ResetPattern() {
        startReset = false;
        allowStop = true;
        waitToAttack = true;
        startCooldown = false;
        Agent.NavAgent.isStopped = false;
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

