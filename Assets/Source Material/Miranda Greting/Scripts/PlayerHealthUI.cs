using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject[] hpSlots;
    [SerializeField]
    private TextMeshProUGUI flaskAmountText;
    [SerializeField]
    private int maxFlasks;
    private int flaskAmount;
    private int health;
    private int maxHealth;
    private HealthScript hpScript;
    private CharacterController flaskScript;
    private int previousHealth;
    private int previousFlaskAmt;

    // Start is called before the first frame update
    void Start()
    {
        hpScript = player.GetComponent<HealthScript>();
        health = (int)hpScript.GetHealth();
        maxHealth = hpSlots.Length; 
        previousHealth = maxHealth;
        flaskScript = player.GetComponent<CharacterController>();
        HealthSetup();
        flaskAmount = maxFlasks;
        previousFlaskAmt = maxFlasks;
        flaskAmountText.text = maxFlasks + "/" + maxFlasks;
    }

    private void FixedUpdate()
    {
        //Update health flask amount
        flaskAmountText.text = (int)flaskScript.GetFlaskUses() + "/" + maxFlasks;

        //check for health value change
        health = (int)hpScript.GetHealth();
        if (health != previousHealth)
        {
            HealthSetup();
        }
        previousHealth = health;
    }

    public void FlaskUsage()
    {
        //decrease available potion amount & update UI 
        if (flaskAmount > 0)
        {
            flaskAmountText.text = flaskAmount + "/" + maxFlasks;
            HealthSetup();
        }
    }

    public void HealthSetup()
    {
        if (health >= 1)
        {
            //sets the EmptyHP gameobject active for amount of lost HP
            for (int i = health - 1; i <= maxHealth - 1; i++)
            {
                hpSlots[i].transform.GetChild(0).gameObject.SetActive(false);
                hpSlots[i].transform.GetChild(2).gameObject.SetActive(false);
                hpSlots[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            //sets the NormalHP gameobject active for the amount of remaining HP
            for (int i = 0; i <= health - 2; i++)
            {
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
}
