using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tobias Martinsson
[CreateAssetMenu()]
public class BossFloorAttackState : State
{
    SomeAgent Agent;
    private float originalTime;
    private Quaternion originalRotation;
    private BossMechanicsScript bossScript;

    protected override void Initialize()
    {
        Agent = (SomeAgent)Owner;
        Debug.Assert(Agent);
        originalRotation = Agent.transform.rotation;
    }

    public override void Enter()
    {
        Agent.transform.rotation = originalRotation;
        bossScript = Agent.GetComponent<BossMechanicsScript>();
        int rand = randomNumber(4);

        if (rand == 0)
        {
            bossScript.FadeIn(bossScript.leftFloor);
        }
        else if (rand == 1)
        {
            bossScript.FadeIn(bossScript.rightFloor);
        }
        else if (rand == 2 || rand == 3)
        {
            bossScript.FadeInCircle();
        }


    }
    public override void RunUpdate()
    {

        StateMachine.ChangeState<BossChooseAttackState>();
    }

    public int randomNumber(int x)
    {
        return Random.Range(0, x);
    }
}
