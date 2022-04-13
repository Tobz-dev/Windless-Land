using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author:Xiehui Mao
public class SoulControl : MonoBehaviour
{

    public GameObject soul;
    public GameObject soulguide;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            soulguide.SetActive(true);
            soul.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            soulguide.SetActive(false);
            soul.SetActive(true);
        }
    }
}
