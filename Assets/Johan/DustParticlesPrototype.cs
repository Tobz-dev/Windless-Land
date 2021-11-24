using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DustParticlesPrototype : MonoBehaviour
{
    [SerializeField]
    private Button hideButton;
    [SerializeField]
    private Button showButton;

    [SerializeField]
    private ParticleSystem dustParticles;
    [SerializeField]
    private Text hideButtonText;
    [SerializeField]
    private Slider densitySlider;

    private bool isHideButton = true;
    private float emissionAmount;


    public void ClickButton()
    {
        if (isHideButton)
        {
            HideParticles();
        }
        else
        {
            ShowParticles();
        }
    }

    private void HideParticles()
    {
        dustParticles.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        hideButtonText.text = "Show";
        isHideButton = false;
    }

    private void ShowParticles()
    {
        dustParticles.Play();
        hideButtonText.text = "Hide";
        isHideButton = true;
    }

    public void ChangeParticleEmission(float newEmissionAmount)
    {
        emissionAmount = newEmissionAmount;
        var emission = dustParticles.emission;
        emission.rateOverTime = emissionAmount;
    }
}
