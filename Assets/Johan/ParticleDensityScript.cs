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


    private void Awake()
    {
        switch (particleDensitySetting)
        {
            case 1:
                ChangeFireToNormal();
                break;
            case 2:
                ChangeFireToReduced();
                break;
            case 3:
                ChangeFireToMinimal();
                break;
            default:
                ChangeFireToNormal();
                break;
        }
    }

    public void SetDensityNormal()
    {
        particleDensitySetting = 1;
        ChangeFireToNormal();
    }

    public void SetDensityReduced()
    {
        particleDensitySetting = 1;
        ChangeFireToReduced();
    }

    public void SetDensityMinimal()
    {
        particleDensitySetting = 1;
        ChangeFireToMinimal();
    }

    private void ChangeFireToNormal()
    {
        for (int i = 0; i < torchFiresNormal.Length; i++)
        {
            torchFiresNormal[i].SetActive(true);
        }

        for (int i = 0; i < torchFiresReduced.Length; i++)
        {
            torchFiresReduced[i].SetActive(false);
        }
    }

    private void ChangeFireToReduced()
    {
        for (int i = 0; i < torchFiresNormal.Length; i++)
        {
            torchFiresNormal[i].SetActive(false);
        }

        for (int i = 0; i < torchFiresReduced.Length; i++)
        {
            torchFiresReduced[i].SetActive(true);
        }
    }

    private void ChangeFireToMinimal()
    {
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
