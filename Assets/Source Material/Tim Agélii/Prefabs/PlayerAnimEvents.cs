using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Main Authour : Tim Agélii
public class PlayerAnimEvents : MonoBehaviour
{
    [SerializeField]
    private GameObject heavyAttackHitbox;

    [SerializeField]
    private GameObject lightAttackHitbox;

    [SerializeField]
    private GameObject arrow;

    private int comboNumber = 1;

    private float playerMoveSpeed;

    private bool allowMovement = true;

    private bool endOfAttack = false;

    private bool EndPlayerStunned = false;
   
    public void instantiateLightHitbox() {

        var newHitbox = Instantiate(lightAttackHitbox, transform.position + (transform.rotation * new Vector3(0, 0.5f, 1.7f)), transform.rotation);

        newHitbox.transform.parent = gameObject.transform;
    }

    public void instantiateHeavyHitbox()
    {

        var newHitbox = Instantiate(heavyAttackHitbox, transform.position + (transform.rotation * new Vector3(0, 0.5f, 1.7f)), transform.rotation);

        newHitbox.transform.parent = gameObject.transform;
    }


    public void InstantiateArrow()
    {
        GameObject arrowPrefab = Instantiate(arrow, transform.position + (transform.rotation * new Vector3(0, 1.5f, 1.5f)), transform.rotation);
    }


    public void SetPlayerMoveSpeed(float f) {
        playerMoveSpeed = f;
    }
    public float GetPlayerMoveSpeed() {
        return playerMoveSpeed;
    }


    public void SetComboNumber(int i)
    {
        comboNumber = i;
    }
    public int GetComboNumber()
    {
        return comboNumber;
    }


    public void SetAllowMovementFalse()
    {
        allowMovement = false;
    }
    public void SetAllowMovementTrue()
    {
        allowMovement = true;
    }
    public bool GetAllowMovement()
    {
        return allowMovement;
    }

    public void SetEndOfAttackTrue() {
        endOfAttack = true;
    }

    public void SetEndOfAttackFalse()
    {
        endOfAttack = false;
    }

    public bool GetEndOfAttack() {
        return endOfAttack;
    }

    public void SetEndPlayerStunnedTrue()
    {
        EndPlayerStunned = true;
    }

    public void SetEndPlayerStunnedFalse()
    {
        EndPlayerStunned = false;
    }

    public bool GetEndPlayerStunned()
    {
        return EndPlayerStunned;
    }

}
