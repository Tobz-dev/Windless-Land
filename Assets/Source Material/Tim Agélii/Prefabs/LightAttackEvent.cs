using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Main Author: Tim Agelii
public class LightAttackEvent : MonoBehaviour
{

    private bool stopAttack = false;

    private float enemySpeed;


    public void SetStopAttack(bool b)
    {
        stopAttack = b;
    }
    public void SetStopAttackTrue()
    {
        stopAttack = true;
    }
    public bool GetStopAttack()
    {
        return stopAttack;
    }

    public void SetEnemySpeed(float f)
    {
        enemySpeed = f;
    }

    public float GetEnemySpeed()
    {
        return enemySpeed;
    }

}
