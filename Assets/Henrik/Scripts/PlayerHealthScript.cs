using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//Main Author: Tim Agelii

//secondary Author: Henrik Ruden, William Smith

public class PlayerHealthScript : MonoBehaviour
{


    private int chilldrenAmount;

    public int health;
    [SerializeField]
    public int Maxhealth;

    [SerializeField]
    private bool startOnMaxHealth = true;
    [SerializeField]
    private Material material;
    private Material originalMaterial;
    //[SerializeField]
    //private GameObject[] hpSlots;
    //[SerializeField]
    //private TextMeshProUGUI flaskAmountText;
    //[SerializeField]
    private int startingFlasks = 0;
    [SerializeField]
    private CameraFollow cameraFollow;

    private int currentFlaskAmount;
    private bool startInvincibilityTimer = false;
    private bool damageIsOnCooldown = false;
    private float invincibilityTimer = 0;

    private FMOD.Studio.EventInstance PlayerHurt;

    [SerializeField]
    private float playerInvincibilityTime;

    private PlayerUI playerUI;


    // Start is called before the first frame update
    void Awake()
    {
        if (gameObject.GetComponent<MeshRenderer>() != null)
        {
            originalMaterial = gameObject.GetComponent<MeshRenderer>().material;
        }

        chilldrenAmount = transform.childCount;
        if (startOnMaxHealth) 
        {
            health = Maxhealth;
        }
        //HealthSetup();
        currentFlaskAmount =  GetComponent<CharacterController>().GetMaxFlaskUses();
        startingFlasks = GetComponent<CharacterController>().GetMaxFlaskUses();

        Debug.Log("in PlayerhealthScript. currentFlaskAmount is: " + currentFlaskAmount);

        Debug.Log("in PlayerhealthScript. startingFlasks is: " + startingFlasks);

        playerUI = FindObjectOfType<PlayerUI>();

    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            //death animation and delay
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

        playerUI.HealthbarSetup(health);
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

        playerUI.HealthbarSetup(health);
        //HealthSetup();
        //Debug.Log("REGAINED" + x + " HEALTH,  health IS NOW " + health);
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
        //currentFlaskAmount = 4;
        //GetComponent<CharacterController>().SetFlaskUses(4);
        //flaskAmountText.text = flaskAmount + "/" + maxFlasks;
    }


    public void SetConfig(int newMaxHealth)
    {
        //Maxhealth = newMaxHealth;
        //health = Maxhealth;
    }
}
