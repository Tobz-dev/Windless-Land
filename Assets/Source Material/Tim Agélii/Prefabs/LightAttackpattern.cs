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
    [SerializeField]
    private float attackChargeTime;
    Vector3 turnDirection;

    //public GameObject hitBox;

    private bool allowStop = true;

    private bool startLastCooldown = false;
    private bool startReset = false;
    private bool startAttack = false;
    private bool startCooldown = false;
    private bool startSwing = false;
    private bool startSecondSwing = false;
    private bool waitToAttack = true;


    //hitbox variables
   
    [SerializeField]
    private float swingDuration;
    [SerializeField]
    private float swingMovespeed;
    [SerializeField]
    private GameObject attackHitbox;

    [SerializeField]
    private float swingCooldownTime;

    [SerializeField]
    private float patternCooldownTime;
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
        if (startSwing == true) {
            Swing();
        }
        if (startSecondSwing) {
            SecondSwing();
        }
        if (startCooldown == true)
        {
            CoolDown();
        }
        if (startLastCooldown == true) {
            LastCooldown();
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

            if (AttackWaitTimer(attackChargeTime))
            {
              
           
                startAttack = false;
                startSwing = true;
                Agent.animator.SetTrigger("StartAttack");


            }

        }
    }
    void CoolDown()
    {
        if (startCooldown == true)
        {
            if (AttackWaitTimer(swingCooldownTime))
            {
                startSecondSwing = true;
                startCooldown = false;

                Agent.animator.SetTrigger("StartAttack");
            }
        }

    }

    void LastCooldown() {
        if (startLastCooldown == true)
        {
            if (AttackWaitTimer(patternCooldownTime))
            {
                startReset = true;
                startLastCooldown = false;
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

    void Swing()
    {
        if (startSwing == true)
        {
            if (AttackWaitTimer(swingDuration))
            {
                InstantiateOneHitbox();
                startSwing = false;
                startCooldown = true;
                Agent.animator.SetTrigger("StopAttack");

            }
            else 
            {
                LookAtPlayer();
                Agent.transform.position += Agent.transform.forward.normalized * swingMovespeed * Time.deltaTime;
            }
        }

    }

    void SecondSwing() {
        if (startSecondSwing == true)
        {
            if (AttackWaitTimer(swingDuration))
            {
                InstantiateOneHitbox();
                startSecondSwing = false;
                startLastCooldown = true;
            }
            else
            {
                LookAtPlayer();
                Agent.transform.position += Agent.transform.forward.normalized * swingMovespeed * Time.deltaTime;
            }
        }
    }
    void InstantiateOneHitbox()
    {

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

