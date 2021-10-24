using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Main Author: Tim Agélii

public class PlayerAttackHitbox : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    private float deathTimer = 0;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.tag == "Enemy")
        {

         
                other.gameObject.GetComponent<HealthScript>().takeDamage(damage);
                Debug.Log("Dealt 1 damage");

                Destroy(this.gameObject);
            

        }
    }

    private void Awake()
    {
        deathTimer = 0;
    }

    private void Update()
    {
        DeathTimer(0.4f);
    }



    private void DeathTimer(float seconds)
    {

        deathTimer += Time.deltaTime;

        if (deathTimer >= seconds)
        {

            Destroy(this.gameObject);


        }

    }
}
