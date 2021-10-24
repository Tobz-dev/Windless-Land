using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Main Author: Henrik Rudén
//secondary Author: Tim Agélii
public class newHitbox : MonoBehaviour
{
    [SerializeField]
    private float swingTime;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private string target;
    private bool invincibility = false;

    private float deathTimer;


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == target && target.Equals("Player"))
        {

            if (other.gameObject.GetComponent<CharacterController>().GetInvincibility() == true)
            {
                invincibility = true;
            }


        }

        if (other.gameObject.tag == target && invincibility == false)
        {
            Debug.Log("Hit " + target);
            other.GetComponent<HealthScript>().takeDamage(damage);
            Debug.Log("Dealt " + damage + " damage");

            Destroy(this.gameObject);
        }
        else if(invincibility == true) {
            invincibility = false;
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
