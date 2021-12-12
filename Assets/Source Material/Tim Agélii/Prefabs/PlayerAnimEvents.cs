using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    [SerializeField]
    private GameObject heavyAttackHitbox;

    [SerializeField]
    private GameObject lightAttackHitbox;

    [SerializeField]
    private GameObject arrow;

    private float playerMoveSpeed;

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
    public float GetplayerMoveSpeed() {
        return playerMoveSpeed;
    }
}
