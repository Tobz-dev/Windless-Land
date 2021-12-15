using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TrapArrow : MonoBehaviour
{
    [SerializeField]
    private VisualEffect arrowEffect;
    [SerializeField]
    private GameObject trailObject;

    void Awake()
    {

        switch (PlayerArrowShowOff.particleDensitySetting)
        {
            case 1:
                trailObject.SetActive(true);
                arrowEffect.SetFloat("SparkCount", 1.6f);
                break;
            case 2:
                trailObject.SetActive(false);
                arrowEffect.SetFloat("SparkCount", 0f);
                break;
            case 3:
                trailObject.SetActive(false);
                arrowEffect.SetFloat("SparkCount", 0f);
                break;
            default:
                trailObject.SetActive(true);
                arrowEffect.SetFloat("SparkCount", 1.6f);
                break;

        }

    }


}
