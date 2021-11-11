using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Main Author: Tim Ag�lii
//statemachine kod tagen fr�n aiattack
[CreateAssetMenu()]
public class HeavyAttackpattern : State
{
    SomeAgent Agent;


    [SerializeField]
    public float outOfRange;


    private float attackTimer = 0;

    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float attackChargeTime;
    Vector3 turnDirection;

    //public GameObject hitBox;

    private bool allowStop = true;


    private bool startAttack = false;
    private bool startCooldown = false;
 

    private bool PrepareAttack = true;


    //hitbox variables
 
    [SerializeField]
    private GameObject attackHitbox;

    [SerializeField]
    private float swingTime;

    [SerializeField]
    private float attackWaitTime;

    [SerializeField]
    private Vector3 hitboxOffset;

    [SerializeField]
    private float xRotationOffset;
    [SerializeField]
    private float yRotationOffset;
    [SerializeField]
    private float zRotationOffset;




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
            Debug.Log(" ATTACK TO CHASE");
            StateMachine.ChangeState<HeavyChase>();
        }
    }

    void AttackPattern()
    {
        if (PrepareAttack == true)
        {
            Agent.NavAgent.isStopped = true;
            AttackChargeUp();
            LookAtPlayer();
        }
        if (startAttack == true) {
            Attack();
            LookAtPlayer();
        }
        if (startCooldown == true) {
            CoolDown();
        }



    }

    void AttackChargeUp()
    {
        allowStop = false;
        if (AttackWaitTimer(attackWaitTime))
        {
            Agent.animator.SetTrigger("Attack");
            startAttack = true;
            PrepareAttack = false;

        }
    }
      

    void LookAtPlayer() {

        turnDirection = Agent.Player.position - Agent.transform.position;
        turnDirection.Normalize();
        Agent.transform.rotation = Quaternion.Slerp(Agent.transform.rotation, Quaternion.LookRotation(turnDirection), turnSpeed * Time.deltaTime);
    }

    void Attack() {
        if (startAttack == true) {
            if (AttackWaitTimer(attackChargeTime))
            {
                
            
                InstantiateOneHitbox();
                startAttack = false;
                startCooldown = true;
             
            
            }
           
        }
    }
    void CoolDown() {
        if (startCooldown == true) {
            if (AttackWaitTimer(swingTime))
            {
                Agent.NavAgent.isStopped = false;
                allowStop = true;
                PrepareAttack = true;
                startCooldown = false;
            }
        }
        
    }

    void InstantiateOneHitbox() {


        GameObject hitBox = (GameObject)Instantiate(attackHitbox, Agent.transform.position + (Agent.transform.rotation * hitboxOffset), Agent.transform.rotation * Quaternion.Euler(xRotationOffset, yRotationOffset, zRotationOffset));

   


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

