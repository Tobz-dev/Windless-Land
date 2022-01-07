using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author:Xiehui Mao
public class WarningControl : MonoBehaviour
{
    public GameObject warning;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            warning.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            warning.SetActive(false);
        }
    }
}
