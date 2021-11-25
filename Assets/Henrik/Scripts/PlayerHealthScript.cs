using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthScript : MonoBehaviour
{


    private int chilldrenAmount;

    public int health;
    [SerializeField]
    public int Maxhealth;
    [SerializeField]
    private Material material;
    private Material originalMaterial;
    [SerializeField]
    private GameObject[] hpSlots;
    [SerializeField]
    private TextMeshProUGUI flaskAmountText;
    [SerializeField]
    private int maxFlasks = 4;
    [SerializeField]
    private CameraFollow cameraFollow;

    private int flaskAmount;
    private bool startInvincibilityTimer = false;
    private bool damageIsOnCooldown = false;
    private float invincibilityTimer = 0;

    private FMOD.Studio.EventInstance PlayerHurt;

    Scene scene;

    [SerializeField]
    private float playerInvincibilityTime;


    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<MeshRenderer>() != null)
        {
            originalMaterial = gameObject.GetComponent<MeshRenderer>().material;
        }

        scene = SceneManager.GetActiveScene();
        chilldrenAmount = transform.childCount;
        health = Maxhealth;
        HealthSetup();
        flaskAmount = maxFlasks;
        
        flaskAmountText.text = maxFlasks + "/" + maxFlasks;
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

            
                HealthSetup();
                damageIsOnCooldown = true;
                startInvincibilityTimer = true;

                cameraFollow.StartShake();

                PlayerHurt = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Player/Hurt");
                PlayerHurt.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                PlayerHurt.start();
                PlayerHurt.release();

                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("TakeDamageEffect", 1);



            
        }


        //update UI healthbar to current player health
        if (gameObject.tag == "Player")
        {

        }
    }


    public void regainHealth(int x)
    {
        if (Maxhealth - health > 0 && x <= Maxhealth - health)
        {
            health += x;
            //decrease available potion amount & update UI 
            flaskAmount--;
            flaskAmountText.text = flaskAmount + "/" + maxFlasks;
        }
        if (Maxhealth - health > 0 && x > Maxhealth - health)
        {
            health = Maxhealth;
        }
        HealthSetup();
        Debug.Log("REGAINED" + x + " HEALTH,  MAX IS NOW " + health);
    }

    private void HealthSetup()
    {
        if (health >= 1)
        {
            //sets the EmptyHP gameobject active for amount of lost HP
            for (int i = health - 1; i <= Maxhealth - 1; i++)
            {
                hpSlots[i].transform.GetChild(0).gameObject.SetActive(false);
                hpSlots[i].transform.GetChild(2).gameObject.SetActive(false);
                hpSlots[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            //sets the NormalHP gameobject active for the amount of remaining HP
            for (int i = 0; i <= health - 2; i++)
            {
                //Debug.Log("yeet");
                hpSlots[i].transform.GetChild(0).gameObject.SetActive(false);
                hpSlots[i].transform.GetChild(1).gameObject.SetActive(false);
                hpSlots[i].transform.GetChild(2).gameObject.SetActive(true);
            }
            //sets the CurrentHP gameobject active at the player's current health
            hpSlots[health - 1].transform.GetChild(2).gameObject.SetActive(false);
            hpSlots[health - 1].transform.GetChild(1).gameObject.SetActive(false);
            hpSlots[health - 1].transform.GetChild(0).gameObject.SetActive(true);
        }
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



    public void ResetPotions()
    {
        flaskAmount = 4;
        GetComponent<CharacterController>().SetFlaskUses(4);
        flaskAmountText.text = flaskAmount + "/" + maxFlasks;
    }

    float GetHealthPercentage()
    {
        return (float)health / (float)Maxhealth;
    }
}
