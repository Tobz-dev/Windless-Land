using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
//Main Author: Johan Nydahl

public class PlayerArrowVFX : MonoBehaviour
{

    [SerializeField]
    private VisualEffect impactEffect;
    [SerializeField]
    private VisualEffect pulseEffect;
    [SerializeField]
    private VisualEffect fireEffect;
    [SerializeField]
    private GameObject[] bodyQuads;
    [SerializeField]
    private GameObject trailObject;


    // Start is called before the first frame update
    void Awake()
    {
        
        switch (ParticleDensityScript.particleDensitySetting)
        {
            case 1:
                trailObject.SetActive(true);
                pulseEffect.SendEvent("PlayPulse");
                fireEffect.SetVector2("FireSparkCount", new Vector2(8, 15));
                fireEffect.SetVector2("FireSmokeCount", new Vector2(4, 7));
                fireEffect.SendEvent("PlayBowFire");
                break;
            case 2:
                trailObject.SetActive(false);
                fireEffect.SetVector2("FireSparkCount", new Vector2(0, 0));
                fireEffect.SetVector2("FireSmokeCount", new Vector2(0, 0));
                fireEffect.SendEvent("PlayBowFire");    
                break;
            case 3:
                trailObject.SetActive(false);
                break;
            default:
                trailObject.SetActive(true);
                pulseEffect.SendEvent("PlayPulse");
                fireEffect.SetVector2("FireSparkCount", new Vector2(8, 15));
                fireEffect.SetVector2("FireSmokeCount", new Vector2(4, 7));
                fireEffect.SendEvent("PlayBowFire");
            break;

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        transform.position += transform.forward * speed * Time.deltaTime;

        killTime += 1 * Time.deltaTime;
        if (killTime >= 5f)
        {
            Destroy(gameObject);
        }
        */
    }

    public void PlayImpactEffect()
    {
        switch (ParticleDensityScript.particleDensitySetting)
        {
            case 1:
                impactEffect.SetVector2("ImpactSparkCount", new Vector2(8, 15));
                impactEffect.SetVector2("ImpactSpark2Count", new Vector2(3, 6));
                impactEffect.SetVector2("ImpactSmokeCount", new Vector2(8, 12));
                impactEffect.SendEvent("PlayImpact");
                break;
            case 2:
                impactEffect.SetVector2("ImpactSparkCount", new Vector2(0, 0));
                impactEffect.SetVector2("ImpactSpark2Count", new Vector2(0, 0));
                impactEffect.SetVector2("ImpactSmokeCount", new Vector2(0, 0));
                impactEffect.SendEvent("PlayImpact");
                break;
            case 3:
                break;
            default:
                impactEffect.SetVector2("ImpactSparkCount", new Vector2(8, 15));
                impactEffect.SetVector2("ImpactSpark2Count", new Vector2(3, 6));
                impactEffect.SetVector2("ImpactSmokeCount", new Vector2(8, 12));
                break;
        }

        for (int i = 0; i < bodyQuads.Length; i++)
        {
            GetComponent<BoxCollider>().enabled = false;
            bodyQuads[i].SetActive(false);
        }
    }
}
