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
    public Transform GetPatrolPoint => PatrolPoints[Random.Range(0, PatrolPoints.Count)];

    public Vector3 PlayerPosition => Player.position;

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
        NavAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        StateMachine = new StateMachine(this, States);

        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        StateMachine.RunUpdate();
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



}
