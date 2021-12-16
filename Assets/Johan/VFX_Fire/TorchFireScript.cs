using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchFireScript : MonoBehaviour
{
    [SerializeField]
    private GameObject fireNormal;
    [SerializeField]
    private GameObject fireReduced;


    private void Awake()
    {
        switch (ParticleDensityScript.particleDensitySetting)
        {
            case 1:
                ChangeToNormal();
                break;
            case 2:
                ChangeToReduced();
                break;
            case 3:
                ChangeToMinimal();
                break;
            default:
                ChangeToNormal();
                break;
        }
    }

    public void ChangeToNormal()
    {
        fireNormal.SetActive(true);
        fireReduced.SetActive(false);
    }

    public void ChangeToReduced()
    {
        fireNormal.SetActive(false);
        fireReduced.SetActive(true);
    }

    public void ChangeToMinimal()
    {
        fireNormal.SetActive(false);
        fireReduced.SetActive(false);
    }
}
