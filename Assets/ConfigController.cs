using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigController : MonoBehaviour
{

    private GameObject player;
    private GameObject fontChanger;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        fontChanger = GameObject.FindGameObjectWithTag("FontChanger");

        //Save();
        Load();
    }

    void Update()
    {
        
    }

    public void Save()
    {
        INIParser ini = new INIParser();
        ini.Open(Application.persistentDataPath + "Config.ini");

        //Player
        ini.WriteValue("Player", "maxMana;", player.gameObject.GetComponent<CharacterController>().getMaxMana());
        ini.WriteValue("Player", "moveSpeed;", player.gameObject.GetComponent<CharacterController>().getMoveSpeed());
        ini.WriteValue("Player", "maxhealth;", player.gameObject.GetComponent<PlayerHealthScript>().getMaxHealth());

        //Font
        ini.WriteValue("Font", "fontSize;", fontChanger.gameObject.GetComponent<FontChange>().getFontIndex());
        ini.WriteValue("Font", "fontIndex;", fontChanger.gameObject.GetComponent<FontChange>().getFontSize());

        ini.Close();
        Debug.Log("Saved Config");
    }

    public void SaveDefault()
    {
        INIParser ini = new INIParser();
        ini.Open(Application.persistentDataPath + "Config.ini");

        //Player
        ini.WriteValue("Player", "maxMana;", 100);
        ini.WriteValue("Player", "moveSpeed;", 5.5f);
        ini.WriteValue("Player", "maxhealth;", 5);

        //Font
        ini.WriteValue("Font", "fontSize;", 5);
        ini.WriteValue("Font", "fontIndex;", 0);

        ini.Close();
        Debug.Log("Saved Config");
    }


    public void Load()
    {
        INIParser ini = new INIParser();
        ini.Open(Application.persistentDataPath + "Config.ini");

        //Player
        player.gameObject.GetComponent<CharacterController>().setConfig(ini.ReadValue("Player", "maxMana;", 100), (float)ini.ReadValue("Player", "moveSpeed;", 5.5f));
        player.gameObject.GetComponent<PlayerHealthScript>().setConfig(ini.ReadValue("Player", "maxhealth;", 5));

        //Font
        fontChanger.gameObject.GetComponent<FontChange>().setConfig(ini.ReadValue("Font", "fontSize;", 5), ini.ReadValue("Font", "fontIndex;", 0));

        ini.Close();
        Debug.Log("Loaded Config");
    }


}
