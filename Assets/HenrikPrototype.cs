using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HenrikPrototype : MonoBehaviour
{
    private bool moreDamage = false;

    private bool cooldownIdikator = false;

    private bool autoAim = false;

    public Image moreDamageCheckbox;
    public Image cooldownIdikatorCheckbox;
    public Image autoAimCheckbox;


    [SerializeField]
    private int addedDamage;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject playerAttackHitbox;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (!System.IO.File.Exists(Application.persistentDataPath + "ProtoConfig.ini"))
        {
            INIParser ini = new INIParser();
            ini.Open(Application.persistentDataPath + "ProtoConfig.ini");
            ini.WriteValue("Henrik", "moreDamage;", 1);
            ini.WriteValue("Henrik", "autoAim;", 0);
            ini.Close();
        }


        SetCheckbox();

        if (moreDamageCheckbox.enabled == true)
        {
            //Debug.Log("More Damage");
            INIParser ini = new INIParser();
            ini.Open(Application.persistentDataPath + "ProtoConfig.ini");
            ini.WriteValue("Henrik", "moreDamage;", 2);
            ini.Close();
        }
        else if (moreDamageCheckbox.enabled == false)
        {
            //Debug.Log("Normal Damage");
            INIParser ini = new INIParser();
            ini.Open(Application.persistentDataPath + "ProtoConfig.ini");
            ini.WriteValue("Henrik", "moreDamage;", 1);
            ini.Close();
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.J))
        {
            SavePrototypeDefault();
        }


        if (autoAimCheckbox.enabled == true)
        {

            //Debug.Log("Auto Aim On");
            INIParser ini = new INIParser();
            ini.Open(Application.persistentDataPath + "ProtoConfig.ini");
            ini.WriteValue("Henrik", "autoAim;", 1);
            ini.Close();

            if (player != null)
            {
                player.gameObject.GetComponent<CharacterController>().updateAutoaim();
            }
            
        }
        else if (autoAimCheckbox.enabled == false)
        {
            //Debug.Log("Auto Aim Off");
            INIParser ini = new INIParser();
            ini.Open(Application.persistentDataPath + "ProtoConfig.ini");
            ini.WriteValue("Henrik", "autoAim;", 0);
            ini.Close();

            if (player != null)
            {
                player.gameObject.GetComponent<CharacterController>().updateAutoaim();
            }
        }



        if (moreDamageCheckbox.enabled == true)
        {
            //Debug.Log("More Damage");
            INIParser ini = new INIParser();
            ini.Open(Application.persistentDataPath + "ProtoConfig.ini");
            ini.WriteValue("Henrik", "moreDamage;", 2);
            ini.Close();
        }
        else if (moreDamageCheckbox.enabled == false)
        {
            //Debug.Log("Normal Damage");
            INIParser ini = new INIParser();
            ini.Open(Application.persistentDataPath + "ProtoConfig.ini");
            ini.WriteValue("Henrik", "moreDamage;", 1);
            ini.Close();
        }



    }

    
    public void autoAimButton()
    {
        autoAim = true;

    }

    public void cooldownIdikatorButton()
    {
        
    }

    public void moreDamageButton()
    {
        
    }

    public void SavePrototypeDefault()
    {
        INIParser ini = new INIParser();
        ini.Open(Application.persistentDataPath + "ProtoConfig.ini");
        ini.WriteValue("Henrik", "autoAim;", 0);
        ini.WriteValue("Henrik", "moreDamage;", 1);
        ini.Close();
        if (player != null)
        {
            player.gameObject.GetComponent<CharacterController>().updateAutoaim();
        }

        autoAimCheckbox.enabled = false;
        moreDamageCheckbox.enabled = false;

        Debug.Log("Saved Default Proto Config");
    }


    public void SetCheckbox()
    {
        INIParser ini = new INIParser();
        ini.Open(Application.persistentDataPath + "ProtoConfig.ini");

        if (ini.ReadValue("Henrik", "moreDamage;", 2) == 2)
        {
            moreDamageCheckbox.enabled = true;
        }

        if (ini.ReadValue("Henrik", "autoAim;", 1) == 1)
        {
            autoAimCheckbox.enabled = true;
        }

        ini.Close();
    }

}
