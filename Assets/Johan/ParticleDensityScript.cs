using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDensityScript : MonoBehaviour
{
    public static int particleDensitySetting = 1;

    [SerializeField]
    private GameObject[] torchFiresNormal;
    [SerializeField]
    private GameObject[] torchFiresReduced;

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

        /*
        for(int i = 0; i < torchFiresNormal.Length; i++)
        {
            torchFiresNormal[i].SetActive(true);
        }

        for (int i = 0; i < torchFiresReduced.Length; i++)
        {
            torchFiresReduced[i].SetActive(false);
        }
        */
    }

    public void SetDensityReduced()
    {
        particleDensitySetting = 2;

        foreach (GameObject enviromentVFX in GameObject.FindGameObjectsWithTag("EnviromentVFX"))
        {
            enviromentVFX.transform.GetChild(0).gameObject.SetActive(false);
            enviromentVFX.transform.GetChild(1).gameObject.SetActive(true);

        }

        /*
        for (int i = 0; i < torchFiresNormal.Length; i++)
        {
            torchFiresNormal[i].SetActive(false);
        }

        for (int i = 0; i < torchFiresReduced.Length; i++)
        {
            torchFiresReduced[i].SetActive(true);
        }
        */
    }

    public void SetDensityMinimal()
    {
        particleDensitySetting = 3;

        foreach (GameObject enviromentVFX in GameObject.FindGameObjectsWithTag("EnviromentVFX"))
        {
            enviromentVFX.transform.GetChild(0).gameObject.SetActive(false);
            enviromentVFX.transform.GetChild(1).gameObject.SetActive(false);

        }

        /*
        for (int i = 0; i < torchFiresNormal.Length; i++)
        {
            torchFiresNormal[i].SetActive(false);
        }

        for (int i = 0; i < torchFiresReduced.Length; i++)
        {
            torchFiresReduced[i].SetActive(false);
        }
        */
    }
}
