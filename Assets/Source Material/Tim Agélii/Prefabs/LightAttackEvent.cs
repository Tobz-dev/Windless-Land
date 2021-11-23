using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttackEvent : MonoBehaviour
{

    private bool stopAttack = false;

    private float enemySpeed;

    private bool flee = false; 

    public void SetStopAttack(bool b) {
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

    public void SetFleeTrue()
    {
        flee = true;
    }
    public void SetFleeFalse()
    {
        flee = false;
    }
    public bool GetFlee()
    {
        return flee;
    }
}
