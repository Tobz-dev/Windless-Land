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

    [SerializeField] private GameObject hpSlot;
    [SerializeField] private GameObject parent;
    private List<GameObject> hpSlotList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        hpScript = player.GetComponent<PlayerHealthScript>();
        maxHealth = (int)hpScript.GetHealth();
        previousHealth = maxHealth;
        health = maxHealth;
        characterController = player.GetComponent<CharacterController>();
        HealthSetup(health, maxHealth);
        maxFlasks = (int)characterController.GetFlaskUses();
        flaskAmount = previousFlaskAmt = maxFlasks;
        flaskAmountText.text = characterController.GetFlaskUses().ToString();
        maxMana = mana = previousMana = (int)characterController.GetMaxMana();
        manaSlider.value = maxMana;
    }


    private void Update()
    {
        flaskAmount = (int)characterController.GetFlaskUses();
        if (flaskAmount != previousFlaskAmt)
        {
            flaskAmountText.text = characterController.GetFlaskUses().ToString();
            previousFlaskAmt = flaskAmount;
        }

        //check for health value change
        health = (int)hpScript.GetHealth();
        if (health != previousHealth)
        {
            HealthSetup(health, maxHealth);
        }

        mana = characterController.GetMana();
        if(mana != previousMana && mana >= 0)
        {
            Debug.Log(mana);
            manaSlider.value = mana;
            previousMana = mana;
            Debug.Log("mana = " + mana);
        }

        //testing
        /*
        if (Input.GetKeyDown(KeyCode.I))
        {
            //LoseHP(health, 1);
            health--;
            HealthSetup(health, maxHealth);
        }
        */

        if (Input.GetKeyDown(KeyCode.L))
        {
            mana -= 20;
        }

        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (health < maxHealth)
            {
                health++;
                HealthSetup(health, maxHealth);
                //FlaskUsage(flaskAmount, health);
            }
        }
        */
        
    }

    public void FlaskUsage(int flaskAmount, int maxFlasks)
    {
        //decrease available potion amount & update UI 
        Debug.Log(flaskAmount);
        flaskAmountText.text = flaskAmount.ToString();
    }

    private void InstantiateHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        //instantiate health slots equal to max health
        for (int i = 0; i <= maxHealth - 1; i++)
        {
            //instantiate hp slots with predetermined space (90 units) inbetween them
            GameObject newHP = Instantiate(hpSlot, new Vector3(-140 + 90 * i, 0, 0), Quaternion.identity) as GameObject;
            newHP.transform.SetParent(parent.transform, false);
            if (i == maxHealth - 1)
            {
                newHP.transform.GetChild(2).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(20, 0, 0);
            }
            hpSlotList.Add(newHP);
        }
    }

    public void HealthSetup(int health, int maxHealth)
    {
        this.health = health;
        if (hpSlotList.Count == 0)
        {
            InstantiateHealth(maxHealth);
        }
        if (health >= 1)
        {
            for (int i = 0; i <= health - 2; i++)
            {
                hpSlotList[i].transform.GetChild(1).gameObject.SetActive(true);
                hpSlotList[i].transform.GetChild(2).gameObject.SetActive(false);
            }

            if (health < previousHealth)
            {
                for (int i = health - 1; i < previousHealth - 1; i++)
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
                GameObject currentHP = hpSlotList[health - 1].transform.GetChild(2).gameObject;
                currentHP.SetActive(true);
                Animator anim = currentHP.transform.GetChild(1).GetComponent<Animator>();
                anim.SetFloat("direction", -1);
                StartCoroutine(DeactivateSlot());
            }
        }

        if (health == 0)
        {
            hpSlotList[health].transform.GetChild(1).gameObject.SetActive(false);
            GameObject activeHP = hpSlotList[health].transform.GetChild(2).gameObject;
            Animator anim = activeHP.transform.GetChild(1).GetComponent<Animator>();
            StartCoroutine(DeactivateSlot());
        }
        previousHealth = health;
    }

    private IEnumerator DeactivateSlot()
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
