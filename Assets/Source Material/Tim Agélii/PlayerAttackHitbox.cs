using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Main Author: Henrik Rud�n
//secondary Author: Tim Ag�lii
public class PlayerAttackHitbox : MonoBehaviour
{
    [SerializeField]
    private GameObject hitboxObject;
    [SerializeField]
    private float swingTime;
    [SerializeField]
    private int damage;
    [SerializeField]
    private string target;
    [SerializeField]
    private int manaPerHit;


    private float deathTimer;

    private Collider hitboxCollider;

    private void Start()
    {
        hitboxCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {

       


        

        if (other.gameObject.tag == target)
        {
            other.GetComponent<EnemyHealthScript>().takeDamage(damage);
           
            GetComponentInParent<CharacterController>().ManaIncreased(manaPerHit);
            Debug.Log("gained " + manaPerHit);

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
            hitboxObject.SetActive(false);
            gameObject.transform.parent = null;
        }

        if(deathTimer >= seconds * 6)
        {
            Destroy(gameObject);
        }

    }


    public void setConfig(int newmanaPerHit)
    {
        manaPerHit = newmanaPerHit;
    }

    public int getManaPerHit()
    {
        return manaPerHit;
    }

    public void setDamage(int newDamage)
    {
            damage = newDamage;
    }
}
