using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Main Author: Tobias Martinsson
[CreateAssetMenu()]
public class AiAttack : State
{
    SomeAgent Agent;
    private float timeLeft = 1;
    private float originalTime;

    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        Debug.Assert(Agent);
        originalTime = timeLeft;
    }

    public override void RunUpdate()
    {

        //play animation / hit swing weapon logic 

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            timeLeft = originalTime;
            StateMachine.ChangeState<AiChasePlayer>();
        }
    }

}

