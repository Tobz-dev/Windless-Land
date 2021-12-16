using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFX : MonoBehaviour
{
    //Potion Effect-----------------------------------------------
    [SerializeField]
    private VisualEffect potionEffect;
    private SkinnedMeshToMesh skinnedMeshToMesh;
    

    //Arrow Effect------------------------------------------------
    private VisualEffect arrowChannelVFX;
    [SerializeField]
    private GameObject arrowChannelObject;
    [SerializeField]
    private GameObject leftHandParent;
    



    private void Start()
    {
        arrowChannelVFX = arrowChannelObject.GetComponent<VisualEffect>();
        skinnedMeshToMesh = GetComponentInChildren<SkinnedMeshToMesh>();

        arrowChannelObject.transform.parent = leftHandParent.transform;
        arrowChannelObject.transform.position = leftHandParent.transform.position;
        arrowChannelObject.transform.rotation = leftHandParent.transform.rotation;
    }

    void Update()
    {
        
    }

    public void PlayArrowChannelEffect()
    {
        switch (PlayerArrowShowOff.particleDensitySetting)
        {
            case 1:
                arrowChannelVFX.SetFloat("OuterPullInCount", 7);
                arrowChannelVFX.SetFloat("InnerPullInCount", 4);
                arrowChannelVFX.SendEvent("PlayBowChannel");
                break;
            case 2:
                arrowChannelVFX.SetFloat("OuterPullInCount", 0);
                arrowChannelVFX.SetFloat("InnerPullInCount", 0);
                arrowChannelVFX.SendEvent("PlayBowChannel");
                break;
            case 3:
                break;
            default:
                arrowChannelVFX.SetFloat("OuterPullInCount", 7);
                arrowChannelVFX.SetFloat("InnerPullInCount", 4);
                arrowChannelVFX.SendEvent("PlayBowChannel");
                break;
        }
    }

    public void StopArrowChannelEffect()
    {
        arrowChannelVFX.SendEvent("StopBowChannel");
    }

    public void PlayArrowFireEffect()
    {
        //GameObject arrowPrefab = Instantiate(playerArrowPrefab, arrowChannelPosition.position, transform.rotation);
        StopArrowChannelEffect();
    }


    public void PlayPotionEffect()
    {
        skinnedMeshToMesh.SetSkinnedMeshAsMesh();

        switch (PlayerArrowShowOff.particleDensitySetting)
        {
            case 1:
                potionEffect.SendEvent("PlayPotionEffect");
                Debug.Log("play effect");
                break;
            case 2:
                potionEffect.SendEvent("PlayPotionEffectReduced");
                break;
            case 3:
                potionEffect.SendEvent("PlayPotionEffectMinimal");
                break;
            default:
                break;
        }
    }

    public void PlayHeavyAttackEffect()
    {

    }

    public void PlayLightAttackEffect()
    {

    }

    public void PlayBloodEffect()
    {

    }

}
