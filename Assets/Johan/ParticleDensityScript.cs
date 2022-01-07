using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Main Author: Johan Nydahl

public class ParticleDensityScript : MonoBehaviour
{
    public static int particleDensitySetting = 1;

    [SerializeField]
    private GameObject[] torchFiresNormal;
    [SerializeField]
    private GameObject[] torchFiresReduced;

    //Hittar alla objekt taggade med EnviromentVFX och sätter det korrekta objektet aktivt
    //Child(0) = normal
    //Child(1) = reduced

    public void SetDensityNormal()
    {
        particleDensitySetting = 1;
        
        foreach(GameObject enviromentVFX in GameObject.FindGameObjectsWithTag("EnviromentVFX"))
        {
            enviromentVFX.transform.GetChild(0).gameObject.SetActive(true);
            enviromentVFX.transform.GetChild(1).gameObject.SetActive(false);

        }
    }

    public void SetDensityReduced()
    {
        particleDensitySetting = 2;

        foreach (GameObject enviromentVFX in GameObject.FindGameObjectsWithTag("EnviromentVFX"))
        {
            enviromentVFX.transform.GetChild(0).gameObject.SetActive(false);
            enviromentVFX.transform.GetChild(1).gameObject.SetActive(true);

        }
    }

    public void SetDensityMinimal()
    {
        particleDensitySetting = 3;

        foreach (GameObject enviromentVFX in GameObject.FindGameObjectsWithTag("EnviromentVFX"))
        {
            enviromentVFX.transform.GetChild(0).gameObject.SetActive(false);
            enviromentVFX.transform.GetChild(1).gameObject.SetActive(false);

        }
    }
}
