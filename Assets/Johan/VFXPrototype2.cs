using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Main Author: Johan Nydahl

public class VFXPrototype2 : MonoBehaviour
{
    [SerializeField]
    private GameObject[] fireSystemsNormal;
    [SerializeField]
    private GameObject[] fireSystemsReduced;
    //--------------------------------------------------
    [SerializeField]
    private GameObject trapArrowNormal;
    [SerializeField]
    private GameObject trapArrowReduced;
    //--------------------------------------------------
    [SerializeField]
    private GameObject fogNormal;
    [SerializeField]
    private GameObject fogReduced;
    //--------------------------------------------------
    [SerializeField]
    private GameObject dustNormal;
    [SerializeField]
    private GameObject normalParticlesSprite;
    [SerializeField]
    private GameObject reducedParticlesSprite;


    public void SwitchToNormal()
    {
        SwitchFireToNormal();
        SwitchTrapArrowToNormal();
        SwitchFogToNormal();
        SwitchDustToNormal();

        normalParticlesSprite.SetActive(true);
        reducedParticlesSprite.SetActive(false);
    }

    public void SwitchToReduced()
    {
        SwitchFireToReduced();
        SwitchTrapArrowToReduced();
        SwitchFogToReduced();
        SwitchDustToReduced();

        normalParticlesSprite.SetActive(false);
        reducedParticlesSprite.SetActive(true);
    }

    private void SwitchFireToNormal()
    {
        for(int i = 0; i < fireSystemsNormal.Length; i++)
        {
            fireSystemsNormal[i].SetActive(true);
            fireSystemsReduced[i].SetActive(false);
        }
    }

    private void SwitchTrapArrowToNormal()
    {
        trapArrowNormal.SetActive(true);
        trapArrowReduced.SetActive(false);
    }

    private void SwitchFogToNormal()
    {
        fogNormal.SetActive(true);
        fogReduced.SetActive(false);
    }

    private void SwitchDustToNormal()
    {
        dustNormal.SetActive(true);
    }

    //--------------------------------------------------

    private void SwitchFireToReduced()
    {
        for (int i = 0; i < fireSystemsNormal.Length; i++)
        {
            fireSystemsNormal[i].SetActive(false);
            fireSystemsReduced[i].SetActive(true);
        }
    }

    private void SwitchTrapArrowToReduced()
    {
        trapArrowNormal.SetActive(false);
        trapArrowReduced.SetActive(true);
    }

    private void SwitchFogToReduced()
    {
        fogNormal.SetActive(false);
        fogReduced.SetActive(true);
    }

    private void SwitchDustToReduced()
    {
        dustNormal.SetActive(false);
    }
}
