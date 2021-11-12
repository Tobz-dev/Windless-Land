using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HPTest : MonoBehaviour
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

    private int normalHP = 0;
    private int activeHP = 2;
    private int emptyHP = 1;

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
        health = Maxhealth = 5;
        health = 4;
        HealthSetup();
        flaskAmount = maxFlasks;
        if (gameObject.tag == "Player")
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

    public void regainHealth(int x)
    {
        if (Maxhealth - health > 0 && x <= Maxhealth - health)
        {
            health += x;
            //decrease available potion amount & update UI 
        }
        if (Maxhealth - health > 0 && x > Maxhealth - health)
        {
            health = Maxhealth;
        }
        flaskAmount--;
        flaskAmountText.text = flaskAmount + "/" + maxFlasks;
        HealthSetup();
        Debug.Log("REGAINED" + x + " HEALTH,  MAX IS NOW " + health);
    }

    private void HealthSetup()
    {
        if (health >= 0 && gameObject.tag == "Player")
        {
            GameObject currentHP;
            //sets the EmptyHP gameobject active for amount of lost HP
            if (health < Maxhealth)
            {
                for (int i = health; i <= Maxhealth - 1; i++)
                {
                    currentHP = hpSlots[i].transform.GetChild(activeHP).gameObject;
                    currentHP.transform.GetChild(1).GetComponent<Animator>().SetTrigger("shatter");
                    currentHP.transform.GetChild(0).gameObject.SetActive(false);
                    StartCoroutine(ShatterTimer(i));
                    hpSlots[i].transform.GetChild(normalHP).gameObject.SetActive(false);

                    //hpSlots[i].transform.GetChild(activeHP).gameObject.SetActive(false);
                    hpSlots[i].transform.GetChild(emptyHP).gameObject.SetActive(true);
                    Debug.Log(health);
                }
            }
            //sets the NormalHP gameobject active for the amount of remaining HP
            for (int i = 0; i <= health - 2; i++)
            {
                //Debug.Log("yeet");
                currentHP = hpSlots[i].transform.GetChild(activeHP).gameObject;

                //currentHP.transform.GetChild(1).GetComponent<Animator>().SetTrigger("restore");
                //currentHP.transform.GetChild(0).gameObject.SetActive(true);

                //hpSlots[i].transform.GetChild(0).gameObject.SetActive(false);
                hpSlots[i].transform.GetChild(emptyHP).gameObject.SetActive(false);
                hpSlots[i].transform.GetChild(activeHP).gameObject.SetActive(false);
                hpSlots[i].transform.GetChild(normalHP).gameObject.SetActive(true);
            }
            //sets the CurrentHP gameobject active at the player's current health
            currentHP = hpSlots[health - 1].transform.GetChild(activeHP).gameObject;

            currentHP.transform.GetChild(1).GetComponent<Animator>().SetTrigger("restore");
            currentHP.transform.GetChild(0).gameObject.SetActive(true);

            hpSlots[health - 1].transform.GetChild(normalHP).gameObject.SetActive(false);
            hpSlots[health - 1].transform.GetChild(emptyHP).gameObject.SetActive(false);
            hpSlots[health - 1].transform.GetChild(activeHP).gameObject.SetActive(true);
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
        if (gameObject.tag != "Player")
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

    public float GetHealth()
    {

        return health;
    }

    private IEnumerator ShatterTimer(int i)
    {
        yield return new WaitForSeconds(3);
        //hpSlots[i].transform.GetChild(normalHP).gameObject.SetActive(false);

    }
}
