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

    public void SetDensityNormal()
    {
        particleDensitySetting = 1;

        for(int i = 0; i < torchFiresNormal.Length; i++)
        {
            torchFiresNormal[i].SetActive(true);
        }

        for (int i = 0; i < torchFiresReduced.Length; i++)
        {
            torchFiresReduced[i].SetActive(false);
        }
    }

    public void SetDensityReduced()
    {
        particleDensitySetting = 1;

        for (int i = 0; i < torchFiresNormal.Length; i++)
        {
            torchFiresNormal[i].SetActive(false);
        }

        for (int i = 0; i < torchFiresReduced.Length; i++)
        {
            torchFiresReduced[i].SetActive(true);
        }
    }

    public void SetDensityMinimal()
    {
        particleDensitySetting = 1;

        for (int i = 0; i < torchFiresNormal.Length; i++)
        {
            torchFiresNormal[i].SetActive(false);
        }

        for (int i = 0; i < torchFiresReduced.Length; i++)
        {
            torchFiresReduced[i].SetActive(false);
        }
    }
}
