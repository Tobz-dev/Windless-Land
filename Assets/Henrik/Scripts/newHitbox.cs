using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Main Author: Henrik Rudén

public class newHitbox : MonoBehaviour
{
    [SerializeField]
    private float swingDelay = 0.5f;
    [SerializeField]
    private float swingTime = 0.4f;
    [SerializeField]
    private int damage = 1;

    public float deathTimer = 0f;

    private string target;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == target)
        {
            Debug.Log("Hit");
            other.GetComponent<HealthScript>().takeDamage(damage);
            Debug.Log("Dealt " + damage + " damage");

            Destroy(this.gameObject);
        }
    }


    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.tag == tt)
        {


            other.gameObject.GetComponent<HealthScript>().takeDamage(damage);
            Debug.Log("Dealt 1 damage");

            Destroy(this.gameObject);


        }
    }*/

    private void Awake()
    {
        deathTimer = 0;
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
