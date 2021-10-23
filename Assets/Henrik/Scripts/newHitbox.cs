using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Main Author: Henrik Rudén

public class newHitbox : MonoBehaviour
{
    private float swingTime;
    private int damage;
    private string target;
    private bool invisibility = false;

    private float deathTimer;


    private void OnTriggerEnter(Collider other)
    {

        if (target == "Player")
        {
            //invisibility = other.gameObject.GetComponent<CharacterController>().GetInvisibility();
        }

        if (other.gameObject.tag == target && invisibility == false)
        {
            Debug.Log("Hit " + target);
            other.GetComponent<HealthScript>().takeDamage(damage);
            Debug.Log("Dealt " + damage + " damage");

            Destroy(this.gameObject);
        }
    }

    private void Awake()
    {
        deathTimer = 0f;
    }


    private void Update()
    {
        DeathTimer(swingTime);
    }


    private void DeathTimer(float seconds)
    {

        deathTimer += Time.deltaTime;

        if (deathTimer >= seconds)
        {

            Destroy(this.gameObject);


        }

    }

    public void SetDamage(int x)
    {
        damage = x;
    }

    public void SetSwingTime(float x)
    {
        swingTime = x;
    }

    public void SetTarget(string x)
    {
        target = x;
    }

}
