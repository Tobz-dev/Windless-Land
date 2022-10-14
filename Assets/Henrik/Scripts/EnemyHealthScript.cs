using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//secondary Author: Henrik Ruden, William Smith

public class EnemyHealthScript : MonoBehaviour
{

    private int chilldrenAmount;

    public int health;
    [SerializeField]
    public int Maxhealth;
    [SerializeField]
    private bool startAtMaxHealth = true;
    [SerializeField]
    private Material material;
    [SerializeField]
    private Material originalMaterial;
    [SerializeField]
    private Slider healthSlider;

    private int flaskAmount;
    private bool startInvincibilityTimer = false;
    private bool damageIsOnCooldown = false;
    private float invincibilityTimer = 0;

    private FMOD.Studio.EventInstance PlayerHurt;
    private FMOD.Studio.EventInstance EnemyHurt;
    private FMOD.Studio.EventInstance EnemyDead;

    public string type;

    Scene scene;

    [SerializeField]
    private float playerInvincibilityTime;

    private void Awake()
    {
        //health = Maxhealth;

        if (System.IO.File.Exists(Application.persistentDataPath + "Config.ini"))
        {
            //UpdateConfig();

        }
    }

    private void Start()
    {
        if (gameObject.GetComponent<MeshRenderer>() != null && gameObject.name != "Boss")
        {
            originalMaterial = gameObject.GetComponent<MeshRenderer>().material;
        }
        else if(gameObject.name == "Boss")
        {
            originalMaterial = GetComponentInChildren<SkinnedMeshRenderer>().material;
        }

        scene = SceneManager.GetActiveScene();
        chilldrenAmount = transform.childCount;
        if (startAtMaxHealth) 
        {
            health = Maxhealth;
        }
        

        
         healthSlider.value = GetHealthPercentage();
        
    }

    // Update is called once per frame
    void Update()
    {


        
            healthSlider.value = GetHealthPercentage();
        

        if (health <= 0)
        {
            //death animation and delay
            EnemyDead = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Small Enemy/SmallEnemyDead");
            EnemyDead.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            EnemyDead.start();
            EnemyDead.release();
            Destroy(gameObject);
        }

        if (startInvincibilityTimer == true)
        {
            if (DamageCooldown(playerInvincibilityTime))
            {
                damageIsOnCooldown = false;
                startInvincibilityTimer = false;
            }
        }
    }

    //damage handler
    public void takeDamage(int x)
    {
        if (damageIsOnCooldown == false)
        {
            health -= x;

            StartCoroutine(damagedMaterial());
            damagedMaterial();
            GetComponent<LightEnemyParticles>().PlayBloodEffect(1);
            
            EnemyHurt = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Small Enemy/SmallEnemyHurt");
            EnemyHurt.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            EnemyHurt.start();
            EnemyHurt.release();
            
        }
    }

    public void regainHealth(int x)
    {
        if (Maxhealth - health > 0 && x <= Maxhealth - health)
        {
            health += x;
            //decrease available potion amount & update UI 
            //what is this doing in the enemy health???
        }
        if (Maxhealth - health > 0 && x > Maxhealth - health)
        {
            health = Maxhealth;
        }
        Debug.Log("REGAINED" + x + " HEALTH,  MAX IS NOW " + health);
    }

    private bool DamageCooldown(float seconds)
    {

        invincibilityTimer += Time.deltaTime;

        if (invincibilityTimer >= seconds)
        {

            invincibilityTimer = 0;

            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("TakeDamageEffect", 0);

            return true;


        }

        return false;
    }

    private IEnumerator damagedMaterial()
    {
        if(gameObject.name != "Boss")
        {
            for (int i = 0; i < chilldrenAmount; i++)
            {

                GameObject child = transform.GetChild(i).gameObject;
                if (child.TryGetComponent(out Renderer renderer) == true)
                {
                    renderer.material = material;
                }

            }
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < chilldrenAmount; i++)
            {

                GameObject child = transform.GetChild(i).gameObject;
                if (child.TryGetComponent(out Renderer renderer) == true)
                {
                    renderer.material = originalMaterial;
                }

            }
        }
        else
        {
            GetComponentInChildren<SkinnedMeshRenderer>().material = material;

            yield return new WaitForSeconds(0.3f);

            GetComponentInChildren<SkinnedMeshRenderer>().material = originalMaterial;
        }
        

    }

    public float GetHealth()
    {

        return health;
    }

    public float GetHealthPercentage()
    {
        return (float)health / (float)Maxhealth;
    }


    public void SetConfig(int newMaxhealth)
    {
        Maxhealth = newMaxhealth;
    }

    public void UpdateConfig()
    {
        if (type == "Light" || type == "Heavy")
        {

            INIParser ini = new INIParser();
            ini.Open(Application.persistentDataPath + "Config.ini");


            if (type == "Light")
            {
                //Maxhealth = ini.ReadValue("Enemy", "LightMaxhealth;", 7);
                //health = Maxhealth;
            }

            if (type == "Heavy")
            {
                //Maxhealth = ini.ReadValue("Enemy", "HeavyMaxhealth;", 12);
                //health = Maxhealth;
            }

            ini.Close();

            //health = Maxhealth;

        }
    }

    public int GetMaxhealth()
    {
        return Maxhealth;
    }

}