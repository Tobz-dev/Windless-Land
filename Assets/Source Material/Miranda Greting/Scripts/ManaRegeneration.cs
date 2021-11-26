using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRegeneration : MonoBehaviour
{
    float delayTime = 0.5f;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        delayTime = delayTime - Time.deltaTime;
        if (other.CompareTag("Player") && delayTime <= 0)
        {
            CharacterController script = other.GetComponent<CharacterController>();
            script.ManaIncreased(script.GetMana()+10);
            delayTime = 0.5f;
        }
    }

}
