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


    public Material attackIndicatorMaterial;
    public Material startMaterial;
    private bool stopAttack = false;

    private float playerToEnemyDistance;
    private Vector3 playerToEnemyDir;

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

    // private bool flee = false;



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
        waitToAttack = false;
        startAttack = true;

    }
    public override void RunUpdate()
    {


        moveSpeed = GetSpeed();
        Agent.NavAgent.speed = moveSpeed;


        stopAttack = GetStopAttack();


        playerToEnemyDir = Agent.transform.position - Agent.PlayerPosition;
        playerToEnemyDistance = Vector3.Distance(Agent.transform.position, Agent.PlayerPosition);



        //flee = GetFlee();
        /*if (flee == true)
        {
            Agent.NavAgent.isStopped = false;
            Agent.NavAgent.SetDestination(Agent.transform.position + playerToEnemyDir);
        }*/

        Agent.NavAgent.SetDestination(Agent.PlayerPosition);




        if (Mathf.Abs(playerToEnemyDistance) <= enemyStoppingDistance)
        {
            Agent.NavAgent.isStopped = true;
        }
        else
        {

            Agent.NavAgent.isStopped = false;
        }



        if ((Mathf.Abs(playerToEnemyDistance) <= enemyMeleeDistance) && inAttack == false)
        {
            inMeleeRange = true;
        }
        else if ((Mathf.Abs(playerToEnemyDistance) > enemyMeleeDistance))
        {
            inMeleeRange = false;
        }






        AttackPattern();

        if ((Vector3.Distance(Agent.transform.position, Agent.PlayerPosition) >= outOfRange) && allowStop)
        {

            StateMachine.ChangeState<LightChase>();

        }
    }

    float GetSpeed()
    {
        return Agent.transform.GetComponentInParent<LightAttackEvent>().GetEnemySpeed();
    }

    bool GetStopAttack()
    {
        return Agent.transform.GetComponentInParent<LightAttackEvent>().GetStopAttack();
    }

    /*bool GetFlee()
    {
        return Agent.transform.GetComponentInParent<LightAttackEvent>().GetFlee();
    }
      void SetFleeFalse() {
       Agent.transform.GetComponentInParent<LightAttackEvent>().SetFleeFalse();
    }
    */
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
            // SetFleeFalse();
            Attack();

        }


        if (stopAttack == true)
        {
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
        // if (Agent.NavAgent.isStopped == true) {
        turnDirection = Agent.Player.position - Agent.transform.position;
        turnDirection.Normalize();
        Agent.transform.rotation = Quaternion.Slerp(Agent.transform.rotation, Quaternion.LookRotation(turnDirection), turnSpeed * Time.deltaTime);
        //     }




    }

    void Attack()
    {


        if (startAttack == true)
        {


            if (inMeleeRange)
            {
                Agent.animator.SetTrigger("StartAttack2");
            }
            else
            {


                Agent.animator.SetTrigger("StartAttack");

            }

            startAttack = false;



        }
    }




    void ResetPattern()
    {
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

