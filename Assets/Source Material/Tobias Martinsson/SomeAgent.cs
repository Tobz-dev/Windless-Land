using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Main Author: Tobias Martinsson
public class SomeAgent : MonoBehaviour
{
    public NavMeshAgent NavAgent;
    public Transform Player;
    public LayerMask CollisionLayer;

    public Transform agentTransform;

    public List<Transform> PatrolPoints;
    public new BoxCollider collider;
    public State[] States;
    public Animator animator;

    private StateMachine StateMachine;
    //public Transform GetPatrolPoint => 

    public Vector3 PlayerPosition => Player.position;

    private bool isDead;

    private void Awake()
    {
        //Sets patrolpoints
        if (gameObject.GetComponentInParent<EnemyRespawnScript>() != null)
        {
            SetPatrolPoints(GetComponentInParent<EnemyRespawnScript>().PatrolPoints);
        }
        else
        {
            SetPatrolPoints(PatrolPoints);
        }
       
        //Sets their colliders, navagent, animator and statemachines. 
        collider = GetComponent<BoxCollider>();
        NavAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        StateMachine = new StateMachine(this, States);

        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log("in agent.");
    }

    private void Update()
    {
        if (!isDead)
        {
            StateMachine.RunUpdate();
        }
        else 
        {
            StateMachine.ChangeState<EnemyDeath>();
            //hmm. this makes it enter the state in update. not ideal.
            //but the enemy stops moving. and doesnt rotate. I call that a success.
            //and death state doesn't have any logic in it so all enemies can share a state.
        }
    }

    public Transform GetPatrolPoint()
    {
        return PatrolPoints[Random.Range(0, PatrolPoints.Count)];
    }

    public Transform GetPatrolPointByindex(int x)
    {
        Transform patPoint = PatrolPoints[x];
        return patPoint;
    }

    public void SetPatrolPoints(List<Transform> pPoints)
    {
        PatrolPoints = pPoints;
    }

    public void SetIsDead(bool b) 
    {
        //Debug.Log("in SA. SetIsDead: " + b);
        isDead = b;
    }

}
