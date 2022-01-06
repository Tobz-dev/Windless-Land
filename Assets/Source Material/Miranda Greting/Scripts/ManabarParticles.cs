using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManabarParticles : MonoBehaviour
{
    [SerializeField] private GameObject manaHadleParticles;
    [SerializeField] private GameObject lowManaParticles;
    private CharacterController playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        if(playerScript.GetMana() == 0 || playerScript.GetMana() == playerScript.GetMaxMana())
        {
            manaHadleParticles.SetActive(false);
        }
        lowManaParticles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.GetMana() >=30 && playerScript.GetMana() < playerScript.GetMaxMana())
        {
            manaHadleParticles.SetActive(true);
            lowManaParticles.SetActive(false);
        }
        else if (playerScript.GetMana() < 30)
        {
            manaHadleParticles.SetActive(false);
            lowManaParticles.SetActive(true);
        }
        else
        {
            manaHadleParticles.SetActive(false);
            lowManaParticles.SetActive(false);
        }
    }
}
