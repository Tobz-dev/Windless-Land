using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{

    private int chilldrenAmount;

    private int health;
    [SerializeField]
    private int Maxhealth;
    [SerializeField]
    private Material material;
    [SerializeField]
    private Material originalMaterial;
    [SerializeField]
    private GameObject[] hpSlots;
    [SerializeField]
    private TextMeshProUGUI flaskAmountText;
    [SerializeField]
    private int maxFlasks = 4;
    private int flaskAmount;
    private bool startInvincibilityTimer = false;
    private bool damageIsOnCooldown = false;
    private float invincibilityTimer = 0;

    private FMOD.Studio.EventInstance PlayerHurt;
    private FMOD.Studio.EventInstance EnemyHurt;
    private FMOD.Studio.EventInstance EnemyDead;

    Scene scene;

    [SerializeField]
    private float playerInvincibilityTime;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        chilldrenAmount = transform.childCount;
        health = Maxhealth;
        HealthSetup();
        flaskAmount = maxFlasks;
        if(gameObject.tag == "Player")
        {
            flaskAmountText.text = maxFlasks + "/" + maxFlasks;
        }
    }

    // Update is called once per frame
    void Update()
    {
     
            if (health <= 0 && gameObject.tag != "Player")
        {
            //death animation and delay
            EnemyDead = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Small Enemy/SmallEnemyDead");
            EnemyDead.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            EnemyDead.start();
            EnemyDead.release();
            Destroy(gameObject);
        }

        if (health <= 0 && gameObject.tag == "Player")
        {
            //death animation and delay
            SceneManager.LoadScene(scene.name);
            gameObject.GetComponent<CharacterController>().Respawn();
            //Debug.Log("Player Dead");
        }

        if (startInvincibilityTimer == true) { 
      if(DamageCooldown(playerInvincibilityTime))
            {
                damageIsOnCooldown = false;
                startInvincibilityTimer = false;
            }
        }
    }

    //damage handler
    public void takeDamage(int x)
    {
        if (damageIsOnCooldown == false){
            health -= x;

            StartCoroutine(damagedMaterial());
            damagedMaterial();

            if (gameObject.tag == "Player")
            PlayerHurt = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Player/Hurt");
            PlayerHurt.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            PlayerHurt.start();
            PlayerHurt.release();
            if (gameObject.tag != "Player")
            EnemyHurt = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Small Enemy/SmallEnemyHurt");
            EnemyHurt.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            EnemyHurt.start();
            EnemyHurt.release();

        }
       
       
        //update UI healthbar to current player health
        if (gameObject.tag == "Player")
        {
            HealthSetup();
            damageIsOnCooldown = true;
            startInvincibilityTimer = true;
        }
    }

    public void regainHealth(int x) {
        if (Maxhealth - health > 0 && x <= Maxhealth - health) {
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
        if (health >= 1 && gameObject.tag == "Player")
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
            return true;

        }

        return false;
    }

    private IEnumerator damagedMaterial()
    {
        if(gameObject.tag != "Player")
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
       
    }

    public float GetHealth() {

        return health;
    }
}
