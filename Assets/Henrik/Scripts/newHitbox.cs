using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Main Author: Henrik Rud�n
//secondary Author: Tim Ag�lii
//Minor additions: Ilirian Zuta
public class newHitbox : MonoBehaviour
{
    [SerializeField]
    private GameObject hitboxObject;
    [SerializeField]
    private float swingTime;
    [SerializeField]
    private int damage;
    [SerializeField]
    private string target;
    private bool invincibility = false;

    private float deathTimer;

    private Collider hitboxCollider;

    [SerializeField] private string Barrel;

    private void Start()
    {
        hitboxCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == target && target.Equals("Player"))
        {
            hitboxCollider.enabled = false;
            if (other.gameObject.GetComponent<CharacterController>().GetInvincibility() == true)
            {
                invincibility = true;
            }


        }

        if (other.gameObject.tag == target && invincibility == false)
        {
            Debug.Log("Hit " + target);
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<PlayerHealthScript>().takeDamage(damage);
            }
            else
            {
                other.GetComponent<EnemyHealthScript>().takeDamage(damage);
            }
            Debug.Log("Dealt " + damage + " damage");

           
        }
        else if(invincibility == true) {
            invincibility = false;
        }

        if(other.gameObject.tag == Barrel)
        {
            other.GetComponent<DestroyBarrels>().destroyBarrel();
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
            if (hitboxObject != null)
            {
                hitboxObject.SetActive(false);
            }

            gameObject.transform.parent = null;
        }

        if (deathTimer >= seconds * 6)
        {
            Destroy(gameObject);
        }

    }


    public void setConfig(int newdamage)
    {
        damage = newdamage;
    }

    public int getDamage()
    {
        return damage;
    }
}
