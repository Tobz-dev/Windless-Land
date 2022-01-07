using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Main Author: Tim Agélii
public class hitBox : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;



    private float deathTimer = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<CharacterController>().GetInvincibility() == false)
            {
                other.gameObject.GetComponent<HealthScript>().takeDamage(damage);


                Destroy(this.gameObject);
            }

        }
    }

    private void Awake()
    {
        deathTimer = 0;
    }

    private void Update()
    {

        DeathTimer(0.5f);
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
