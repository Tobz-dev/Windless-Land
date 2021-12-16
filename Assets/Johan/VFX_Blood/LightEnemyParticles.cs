using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class LightEnemyParticles : MonoBehaviour
{

    [SerializeField]
    private VisualEffect bloodEffect;
    private Vector3 currentBloodValues;
    private Vector3 slash1BloodValues = new Vector3(4f, 2.5f, 0.5f);
    private Vector3 slash2BloodValues = new Vector3(-4f, 2.5f, 0.5f);
    private Vector3 slash3BloodValues = new Vector3(2f, 3f, -4f);
    private bool bloodValuesReversed;


    // Start is called before the first frame update
    void Start()
    {
        currentBloodValues = bloodEffect.GetVector3("BloodVelocity");
    }


    public void PlayBloodEffect(int slashNumber)
    {
        /*/
        //Byter riktning på blodet beroende på vilken av spelarens attacker som används
        switch(slashNumber){
            case 1:
                currentBloodValues = slash1BloodValues;
                if (bloodValuesReversed)
                {
                    ReverseBloodValues();
                }
                break;
            case 2:
                currentBloodValues = slash2BloodValues;
                if (bloodValuesReversed)
                {
                    ReverseBloodValues();
                }
                break;
            case 3:
                currentBloodValues = slash3BloodValues;
                ReverseBloodValues();
                break;
            default:
                currentBloodValues = slash3BloodValues;
                ReverseBloodValues();
                break;
        }

        bloodEffect.SetVector3("BloodVelocity", currentBloodValues);
        */
        
        switch (ParticleDensityScript.particleDensitySetting)
        {
            case 1:
                bloodEffect.SetFloat("ImpactSparkCount", 4);
                bloodEffect.SetFloat("BurstCountMin", 20);
                bloodEffect.SetFloat("BurstCountMax", 60);
                break;
            case 2:
                bloodEffect.SetFloat("ImpactSparkCount", 0);
                bloodEffect.SetFloat("BurstCountMin", 5);
                bloodEffect.SetFloat("BurstCountMax", 15);
                break;
            case 3:
                bloodEffect.SetFloat("ImpactSparkCount", 0);
                bloodEffect.SetFloat("BurstCountMin", 0);
                bloodEffect.SetFloat("BurstCountMax", 0);
                break;
            default:
                bloodEffect.SetFloat("ImpactSparkCount", 4);
                bloodEffect.SetFloat("BurstCountMin", 10);
                bloodEffect.SetFloat("BurstCountMax", 40);
                break;

        }
        
        bloodEffect.SendEvent("TriggerBlood");
    }

    private void ReverseBloodValues()
    {
        //Multiplicerar multipler med -1 för att partiklarna ska gå åt rätt håll när de åker bakåt.
        bloodEffect.SetFloat("BloodVelocity0.5Multiplier", (bloodEffect.GetFloat("BloodVelocity0.5Multiplier") * -1));
        bloodEffect.SetFloat("BloodVelocity1Multiplier", (bloodEffect.GetFloat("BloodVelocity1Multiplier") * -1));
        bloodValuesReversed = !bloodValuesReversed;
    }

}
