using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Main Authour : Tim Agélii
public class PlayerAnimEvents : MonoBehaviour
{
    [SerializeField]
    private GameObject heavyAttackHitbox;

    [SerializeField]
    private GameObject lightAttackHitbox1;
    [SerializeField]
    private GameObject lightAttackHitbox2;
    [SerializeField]
    private GameObject lightAttackHitbox3;

    [SerializeField]
    private GameObject arrow;

    private int comboNumber = 1;

    private float playerMoveSpeedFactor;

    private bool allowMovement = true;

    private bool endOfAttack = false;

    private bool endPlayerStunned = false;

    private bool leverPulled = false;

    private bool doneDrinkingPot = false;
    public void instantiateLightHitbox1() {

        var newHitbox = Instantiate(lightAttackHitbox1, transform.position + (transform.rotation * new Vector3(0, 0.5f, 1.7f)), transform.rotation);

        newHitbox.transform.parent = gameObject.transform;
    }

    public void instantiateLightHitbox2()
    {

        var newHitbox = Instantiate(lightAttackHitbox2, transform.position + (transform.rotation * new Vector3(0, 0.5f, 1.7f)), transform.rotation);

        newHitbox.transform.parent = gameObject.transform;
    }

    public void instantiateLightHitbox3()
    {

        var newHitbox = Instantiate(lightAttackHitbox3, transform.position + (transform.rotation * new Vector3(0, 0.5f, 1.7f)), transform.rotation);

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


    public void SetPlayerMoveSpeedFactor(float f) {
        playerMoveSpeedFactor = f;
    }
    public float GetPlayerMoveSpeedFactor() {
        return playerMoveSpeedFactor;
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
        endPlayerStunned = true;
    }

    public void SetEndPlayerStunnedFalse()
    {
        endPlayerStunned = false;
    }

    public bool GetEndPlayerStunned()
    {
        return endPlayerStunned;
    }

    public void SetLeverPulledTrue()
    {
        leverPulled = true;
    }

    public void SetLeverPulledFalse()
    {
        leverPulled = false;
    }

    public bool GetLeverPulled()
    {
        return leverPulled;
    }
    public void SetDoneDrinkingPotTrue()
    {
        doneDrinkingPot = true;
    }

    public void SetDoneDrinkingPotFalse()
    {
        doneDrinkingPot = false;
    }

    public bool GetDoneDrinkingPot()
    {
        return doneDrinkingPot;
    }

}
