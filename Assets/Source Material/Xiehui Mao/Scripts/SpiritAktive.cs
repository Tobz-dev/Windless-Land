using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritAktive : MonoBehaviour
{
    public GameObject spirit;

    private bool activ;

    void Start()
    {
        activ = false;
    }

    public void OnButtonPress()
    {
        if (activ == false)
        {
            spirit.SetActive(true);
            activ = !activ;
        }
        else
        {
            spirit.SetActive(false);
            activ = !activ;
        }
    }
}
