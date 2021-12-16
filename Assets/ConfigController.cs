using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigController : MonoBehaviour
{

    private GameObject player;
    private GameObject fontChanger;


    [SerializeField]
    private GameObject hitboxLight;
    [SerializeField]
    private GameObject hitboxHeavy;
    [SerializeField]
    private GameObject hitboxPlayer;
    [SerializeField]
    private GameObject enemyLight;
    [SerializeField]
    private GameObject enemyHeavy;

    public GameObject[] enemies;


    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        fontChanger = GameObject.FindGameObjectWithTag("FontChanger");

        //Save();
        Load();
        Save();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("KEY TEST");
            //enemyLight.GetComponent<EnemyHealthScript>().setConfig(12);
            //Load();
        }
    }

    public void Save()
    {
        INIParser ini = new INIParser();
        ini.Open(Application.persistentDataPath + "Config.ini");

        //Player
        if (player != null)
        {
            ini.WriteValue("Player", "maxMana;", player.gameObject.GetComponent<CharacterController>().getMaxMana());
            ini.WriteValue("Player", "moveSpeed;", player.gameObject.GetComponent<CharacterController>().getMoveSpeed());
            ini.WriteValue("Player", "maxhealth;", player.gameObject.GetComponent<PlayerHealthScript>().getMaxHealth());
            ini.WriteValue("Player", "manaPerHit;", hitboxPlayer.gameObject.GetComponent<PlayerAttackHitbox>().getManaPerHit());
        }


        //Font
        if (fontChanger != null)
        {
        ini.WriteValue("Font", "fontSize;", fontChanger.gameObject.GetComponent<FontChange>().getFontIndex());
        ini.WriteValue("Font", "fontIndex;", fontChanger.gameObject.GetComponent<FontChange>().getFontSize());
        }
        //Enemy
        ini.WriteValue("Enemy", "LightDamage;", hitboxLight.gameObject.GetComponent<newHitbox>().getDamage());
        ini.WriteValue("Enemy", "HeavyDamage;", hitboxHeavy.gameObject.GetComponent<newHitbox>().getDamage());
        ini.WriteValue("Enemy", "LightMaxhealth;", enemyLight.gameObject.GetComponent<EnemyHealthScript>().getMaxhealth());
        ini.WriteValue("Enemy", "HeavyMaxhealth;", enemyHeavy.gameObject.GetComponent<EnemyHealthScript>().getMaxhealth());


        ini.Close();
        Debug.Log("Saved Config");
    }

    public void SaveDefault()
    {
        INIParser ini = new INIParser();
        ini.Open(Application.persistentDataPath + "Config.ini");

        //Player
        if (player != null)
        {
            ini.WriteValue("Player", "maxMana;", 100);
            ini.WriteValue("Player", "moveSpeed;", 5.5f);
            ini.WriteValue("Player", "maxhealth;", 5);
            ini.WriteValue("Player", "manaPerHit;", 10);
        }

        //Font
        if (fontChanger != null)
        {
            ini.WriteValue("Font", "fontSize;", 5);
            ini.WriteValue("Font", "fontIndex;", 0);
        }
        //Enemy
        ini.WriteValue("Enemy", "LightDamage;", 1);
        ini.WriteValue("Enemy", "HeavyDamage;", 2);
        ini.WriteValue("Enemy", "LightMaxhealth;", 7);
        ini.WriteValue("Enemy", "HeavyMaxhealth;", 12);

        ini.Close();
        Debug.Log("Saved Config");
    }


    public void Load()
    {
        INIParser ini = new INIParser();
        ini.Open(Application.persistentDataPath + "Config.ini");

        //Player
        Debug.Log(player);
        if (player != null)
        {
            Debug.Log(player);
            player.gameObject.GetComponent<CharacterController>().setConfig(ini.ReadValue("Player", "maxMana;", 100), (float)ini.ReadValue("Player", "moveSpeed;", 5.5f));
            player.gameObject.GetComponent<PlayerHealthScript>().setConfig(ini.ReadValue("Player", "maxhealth;", 5));
            hitboxPlayer.gameObject.GetComponent<PlayerAttackHitbox>().setConfig(ini.ReadValue("Player", "manaPerHit;", 10));
        }

        if (fontChanger != null)
        {
            //Font
            fontChanger.gameObject.GetComponent<FontChange>().setConfig(ini.ReadValue("Font", "fontSize;", 5), ini.ReadValue("Font", "fontIndex;", 0));
        }

        //Enemy
        hitboxLight.gameObject.GetComponent<newHitbox>().setConfig(ini.ReadValue("Enemy", "LightDamage;", 1));
        hitboxHeavy.gameObject.GetComponent<newHitbox>().setConfig(ini.ReadValue("Enemy", "HeavyDamage;", 2));

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.gameObject.GetComponent<EnemyHealthScript>().updateConfig();
        }



        ini.Close();
        Debug.Log("Loaded Config");
    }


}
