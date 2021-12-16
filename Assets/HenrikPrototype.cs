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
            player = GameObject.FindGameObjectWithTag("Player");
            autoAim = true;

            INIParser ini = new INIParser();
            ini.Open(Application.persistentDataPath + "ProtoConfig.ini");
            ini.WriteValue("Henrik", "autoAim;", true);
            ini.Close();

            //player.gameObject.GetComponent<CharacterController>().setAutoaim(true);
        }
        else
        {
            //Debug.Log("Auto Aim Off");
            player = GameObject.FindGameObjectWithTag("Player");
            autoAim = false;

            INIParser ini = new INIParser();
            ini.Open(Application.persistentDataPath + "ProtoConfig.ini");
            ini.WriteValue("Henrik", "autoAim;", false);
            ini.Close();

            //player.gameObject.GetComponent<CharacterController>().setAutoaim(false);
        }

        if (player != null)
        {
            player.gameObject.GetComponent<CharacterController>().updateAutoaim();
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

            INIParser ini = new INIParser();
            ini.Open(Application.persistentDataPath + "ProtoConfig.ini");
            ini.WriteValue("Henrik", "moreDamage;", 1);
            ini.Close();

            //playerAttackHitbox.gameObject.GetComponent<PlayerAttackHitbox>().setDamage(1);
        }
        else
        {
            moreDamage = false;

            INIParser ini = new INIParser();
            ini.Open(Application.persistentDataPath + "ProtoConfig.ini");
            ini.WriteValue("Henrik", "moreDamage;", 2);
            ini.Close();

            //playerAttackHitbox.gameObject.GetComponent<PlayerAttackHitbox>().setDamage(2);
        }
    }

}
