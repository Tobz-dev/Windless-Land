using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRegeneration : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController script = other.GetComponent<CharacterController>();
            script.ManaIncreased(script.GetMaxMana());
        }
    }

}
