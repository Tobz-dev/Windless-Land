using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
 
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

    private void Start()
    {
        health = Maxhealth;
        HealthSetup();
        flaskAmount = maxFlasks;
        flaskAmountText.text = maxFlasks + "/" + maxFlasks;
    }

    // Update is called once per frame
    void Update()
    {
     
            if (health <= 0 && gameObject.tag != "Player")
        {
            //death animation and delay
            Destroy(gameObject);
        }

        if (health <= 0 && gameObject.tag == "Player")
        {
            //death animation and delay
            gameObject.GetComponent<CharacterController>().Respawn();
            //Debug.Log("Player Dead");
        }
    }

    //damage handler
    public void takeDamage(int x)
    {
        health -= x;
        StartCoroutine(damagedMaterial()); 
        damagedMaterial();
        //update UI healthbar to current player health
        if (gameObject.tag == "Player")
        {
            HealthSetup();
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
                Debug.Log("yeet");
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

    private IEnumerator damagedMaterial()
    {
        if(gameObject.tag != "Player")
        {
            gameObject.GetComponent<MeshRenderer>().material = material;
            yield return new WaitForSeconds(0.3f);
            gameObject.GetComponent<MeshRenderer>().material = originalMaterial;
        }
       
    }
}
