using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tobias Martinsson
[CreateAssetMenu()]
public class BossFloorAttackState : State
{
    SomeAgent Agent;
    public GameObject arrowPrefab;
    private float originalTime;

    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        Debug.Assert(Agent);
    }

    public override void Enter()
    {
        Debug.Log("Attack #1. Floor attack.");
    }
    public override void RunUpdate()
    {
    
        Agent.GetComponent<BossMechanicsScript>().fadeIn(Agent.GetComponent<BossMechanicsScript>().leftFloor);
       
    }
}
