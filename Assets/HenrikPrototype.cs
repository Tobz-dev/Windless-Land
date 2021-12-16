using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenrikPrototype : MonoBehaviour
{
    private bool moreDamage = false;

    private bool cooldownIdikator = false;

    private bool autoAim = false;

    [SerializeField]
    private int addedDamage;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject playerAttackHitbox;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void autoAimButton()
    {
        if(autoAim == false)
        {
            //Debug.Log("Auto Aim On");
            autoAim = true;
        }
        else
        {
            //Debug.Log("Auto Aim Off");
            autoAim = false;
        }
    }

    public void cooldownIdikatorButton()
    {
        if (cooldownIdikator == false)
        {
            //Debug.Log("Auto Aim On");
            cooldownIdikator = true;
        }
        else
        {
            //Debug.Log("Auto Aim Off");
            cooldownIdikator = false;
        }
    }

    public void moreDamageButton()
    {
        if (moreDamage == false)
        {
            moreDamage = true;
            playerAttackHitbox.gameObject.GetComponent<PlayerAttackHitbox>().setDamage(1);
        }
        else
        {
            moreDamage = false;
            playerAttackHitbox.gameObject.GetComponent<PlayerAttackHitbox>().setDamage(2);
        }
    }

}
