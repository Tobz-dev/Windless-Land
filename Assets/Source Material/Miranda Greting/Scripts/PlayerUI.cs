//Main Author: Miranda Greting
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Slider manaSlider;
    [SerializeField]
    private TextMeshProUGUI flaskAmountText;
    private int maxFlasks;
    private int flaskAmount;
    private int health;
    private int maxHealth;
    private int mana;
    private int maxMana;
    private PlayerHealthScript hpScript;
    private CharacterController characterController;
    private int previousHealth;
    private int previousFlaskAmt;
    private int previousMana;

    [SerializeField]
    private Slider healthSlider;


    [SerializeField] private GameObject hpSlot;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject lowHPParticles;
    private List<GameObject> hpSlotList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        hpScript = player.GetComponent<PlayerHealthScript>();

        maxHealth = (int)hpScript.GetMaxHealth();

        healthSlider.maxValue = maxHealth;

        health = (int)hpScript.GetHealth();
        characterController = player.GetComponent<CharacterController>();
        //HealthSetup(health, maxHealth);
        HealthbarSetup(health);

        maxFlasks = (int)characterController.GetMaxFlaskUses();
        flaskAmount = previousFlaskAmt = maxFlasks;
        //flaskAmountText.text = characterController.GetFlaskUses().ToString();
        ChangeFlaskUI(flaskAmount);


        characterController.SetPlayerUI(this);
        manaSlider.maxValue = characterController.GetMaxMana();

        ManabarSetup(characterController.GetMana());
    }


    private void Update()
    {
        if (characterController.enabled)
        {
            flaskAmount = (int)characterController.GetFlaskUses(); //get amount of flasks available

            if (flaskAmount != previousFlaskAmt) //check if flask amount has been changed
            {
                //flaskAmountText.text = characterController.GetFlaskUses().ToString(); //update flask amount in UI
                ChangeFlaskUI(flaskAmount);
                previousFlaskAmt = flaskAmount;
            }
        }
        else
        {
            //Debug.Log("Activate CharacterController script!!!");
        }

        if (characterController.enabled)
        {
            mana = characterController.GetMana();

        }
    }

    public void FlaskUsage(int flaskAmount)
    {
        //change available potion amount & update UI 
        Debug.Log(flaskAmount);
        //flaskAmountText.text = flaskAmount.ToString();
        ChangeFlaskUI(flaskAmount);
    }

    private void ChangeFlaskUI(int flaskAmount) 
    {
        //new format to show max. since the player can increase their max.
        maxFlasks = (int)characterController.GetMaxFlaskUses();
        int currentValue = (int)characterController.GetFlaskUses();
        //Debug.Log("in PlayerUI, CFU. maxFlasks is: " + maxFlasks);
        //Debug.Log("in PlayerUI, CFU. testValue is: " + currentValue);
        flaskAmountText.text = currentValue + "/" + maxFlasks;
    }

    private void InstantiateHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        //instantiate health slots equal to max health
        for (int i = 0; i <= maxHealth - 1; i++)
        {
            //instantiate hp slots with predetermined space (90 units) inbetween them
            GameObject newHP = Instantiate(hpSlot, new Vector3(-190 + 90 * i, 0, 0), Quaternion.identity) as GameObject;
            newHP.transform.SetParent(parent.transform, false);
            if (i == maxHealth - 1)
            {
                newHP.transform.GetChild(2).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(20, 0, 0);
            }
            hpSlotList.Add(newHP);
        }
    }

    //Using a healthbar instead. easier to change value of it.
    public void HealthbarSetup(int healthbarValue) 
    {
        //Debug.Log("in PlayerUI, HealthbarSetUp. health is: " + healthbarValue);
        healthSlider.value = healthbarValue;
        //Debug.Log("in PlayerUI, HealthbarSetUp. healthSlider.value is: " + healthSlider.value);
    }

    public void ManabarSetup(int manabarValue) 
    {
       
       manaSlider.value = manabarValue;
       //Debug.Log("in PlayerUI, ManabarSetup. manabarValue is: " + manabarValue);
    }


    public void HealthSetup(int health, int maxHealth)
    {
        this.health = health;
        if (hpSlotList.Count == 0) //checks if HP is isn't instantiated
        {
            InstantiateHealth(maxHealth); // instantiates healthbar slots depending on what maxhealth is
        }
        if (health >= 1)
        {
            for (int i = 0; i <= health - 2; i++) //sets NormalHP gameobject active for all healthslots below current health
            {
                if(hpSlotList[i].transform.GetChild(0).gameObject == lowHPParticles)
                {
                    Destroy(hpSlotList[i].transform.GetChild(0).gameObject);
                }
                hpSlotList[i].transform.GetChild(1).gameObject.SetActive(true);
                hpSlotList[i].transform.GetChild(2).gameObject.SetActive(false);
            }

            if (health < previousHealth) 
            {
                for (int i = health - 1; i < previousHealth - 1; i++) //sets EmptyHP active for all slots above currentHP
                {
                    hpSlotList[i + 1].transform.GetChild(1).gameObject.SetActive(false);
                    hpSlotList[i + 1].transform.GetChild(2).gameObject.SetActive(false);
                    GameObject activeHP = hpSlotList[maxHealth - 1].transform.GetChild(2).gameObject;
                    if(health != maxHealth)
                    {
                        activeHP = hpSlotList[health].transform.GetChild(2).gameObject;
                    }
                    Animator anim = activeHP.transform.GetChild(1).GetComponent<Animator>();
                    anim.SetFloat("direction", 1);
                    StartCoroutine(DeactivateSlot());
                }
            }

            if (previousHealth < health) 
            {
                GameObject currentHP = hpSlotList[health - 1].transform.GetChild(2).gameObject; //sets CurrentHP gameobject active for current HP
                currentHP.SetActive(true);
                Animator anim = currentHP.transform.GetChild(1).GetComponent<Animator>();
                anim.SetFloat("direction", -1);
                StartCoroutine(DeactivateSlot());
            }
        }

        if (health == 0) //activates final hp slot if health == 0
        {
            hpSlotList[health].transform.GetChild(1).gameObject.SetActive(false);
            GameObject activeHP = hpSlotList[health].transform.GetChild(2).gameObject;
            Animator anim = activeHP.transform.GetChild(1).GetComponent<Animator>();
            StartCoroutine(DeactivateSlot());
        }
        previousHealth = health;


        if(health == 1) //activates particlesystem to indicate low HP for last HPSlot
        {
            hpSlotList[0].transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
            hpSlotList[1].transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
        }

        if (health == 2) //activates low HP particlesystem for two last HPSlots
        {
            hpSlotList[1].transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
            hpSlotList[0].transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
        }
    }

    private IEnumerator DeactivateSlot() //Animations were supposed to run here but didn't get it to work in time, for a while there was a shatter effect
    {
        GameObject activeHP = hpSlotList[maxHealth - 1].transform.GetChild(2).gameObject;
        if (health < maxHealth)
        {
            activeHP = hpSlotList[health].transform.GetChild(2).gameObject;
            Animator anim = activeHP.transform.GetChild(1).GetComponent<Animator>();
            anim.SetTrigger("shatter");
            yield return new WaitForSeconds(0.25f);
            activeHP.SetActive(false);
        }
        if (health > 0)
        {
            GameObject currentHP = hpSlotList[health - 1].transform.GetChild(2).gameObject;
            currentHP.SetActive(true);
            currentHP.transform.GetChild(1).GetComponent<Animator>().SetTrigger("restore");
        }
    }
}
