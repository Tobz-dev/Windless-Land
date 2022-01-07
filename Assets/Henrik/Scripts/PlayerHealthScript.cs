using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


//Main Author: Tim Agelii

//secondary Author: Henrik Ruden

public class PlayerHealthScript : MonoBehaviour
{


    private int chilldrenAmount;

    public int health;
    [SerializeField]
    public int Maxhealth;
    [SerializeField]
    private Material material;
    private Material originalMaterial;
    //[SerializeField]
    //private GameObject[] hpSlots;
    //[SerializeField]
    //private TextMeshProUGUI flaskAmountText;
    [SerializeField]
    private int startingFlasks = 2;
    [SerializeField]
    private CameraFollow cameraFollow;

    private int currentFlaskAmount;
    private bool startInvincibilityTimer = false;
    private bool damageIsOnCooldown = false;
    private float invincibilityTimer = 0;

    private FMOD.Studio.EventInstance PlayerHurt;

    Scene scene;

    [SerializeField]
    private float playerInvincibilityTime;


    // Start is called before the first frame update
    void Awake()
    {
        if (gameObject.GetComponent<MeshRenderer>() != null)
        {
            originalMaterial = gameObject.GetComponent<MeshRenderer>().material;
        }

        scene = SceneManager.GetActiveScene();
        chilldrenAmount = transform.childCount;
        health = Maxhealth;
        //HealthSetup();
        currentFlaskAmount = startingFlasks;
        
        //flaskAmountText.text = maxFlasks + "/" + maxFlasks;
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            //death animation and delay
            //SceneManager.LoadScene(scene.name);
            gameObject.GetComponent<CharacterController>().Respawn();
            //Debug.Log("Player Dead");
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


    public void takeDamage(int x)
    {
        if (damageIsOnCooldown == false)
        {

            health -= x;
            GetComponent<PlayerVFX>().PlayBloodEffect();
            
                //HealthSetup();
                damageIsOnCooldown = true;
                startInvincibilityTimer = true;

                cameraFollow.StartShake();

                PlayerHurt = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Player/Hurt");
                PlayerHurt.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                PlayerHurt.start();
                PlayerHurt.release();

                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("TakeDamageEffect", 1);

                gameObject.GetComponent<CharacterController>().StartPlayerStun();



        }


        //update UI healthbar to current player health
        if (gameObject.tag == "Player")
        {

        }
    }


    public void regainHealth(int x)
    {

        if (health > Maxhealth || x > (Maxhealth - health))
        {
            health = Maxhealth;
        }

        if (x <= Maxhealth - health)
        {
            health += x;
            //decrease available potion amount & update UI 
            currentFlaskAmount--;
            //flaskAmountText.text = flaskAmount + "/" + maxFlasks;
        }
      
        //HealthSetup();
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

    public float GetHealth()
    {

        return health;
    }

    public float GetMaxHealth()
    {

        return Maxhealth;
    }



    public void ResetPotions()
    {
        currentFlaskAmount = 4;
        GetComponent<CharacterController>().SetFlaskUses(4);
        //flaskAmountText.text = flaskAmount + "/" + maxFlasks;
    }


    public void setConfig(int newMaxHealth)
    {
        Maxhealth = newMaxHealth;
        health = Maxhealth;
    }

    public int getMaxHealth()
    {
        return Maxhealth;
    }
}
