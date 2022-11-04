//Main Author: Miranda Greting
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManabarParticles : MonoBehaviour
{
    [SerializeField] private GameObject manaHadleParticles;
    [SerializeField] private GameObject lowManaParticles;
    private CharacterController playerScript;
    // Start is called before the first frame update
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        if(playerScript.GetMana() == 0 || playerScript.GetMana() == playerScript.GetMaxMana())
        {
            manaHadleParticles.SetActive(false);
        }
        lowManaParticles.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerScript.GetMana() >=50)
        {
            manaHadleParticles.SetActive(true);
            lowManaParticles.SetActive(false);
        }
        else if (playerScript.GetMana() < 50)
        {
            manaHadleParticles.SetActive(false);
            lowManaParticles.SetActive(true);
        }
        else if(playerScript.GetMana() <= 0)
        {
            manaHadleParticles.SetActive(false);
            lowManaParticles.SetActive(false);
        }
        else
        {
            manaHadleParticles.SetActive(false);
            lowManaParticles.SetActive(false);
        }
    }
}
