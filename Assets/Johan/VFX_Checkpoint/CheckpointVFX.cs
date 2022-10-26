using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
//Main Author: Johan Nydahl
//Secondary Author: William Smith

public class CheckpointVFX : MonoBehaviour
{
    [SerializeField]
    private VisualEffect[] checkpointGraphs;
    [SerializeField]
    private GameObject shinePlanes;
    [SerializeField]
    private GameObject shockwaveQuads;

    [SerializeField]
    private Material shockwaveShaderMaterial;
    [SerializeField]
    private Material shineShaderMaterial;

    private bool playShader;
    private bool playShine;
    private bool stopShine;
    private bool reduceForce;
    private bool reduceSpawnRate;
    private float shineScaleCurrent = 2.5f;
    private float shinePositionCurrent = 0.6f;
    private float shineMaskPowerCurrent = 15;
    private float shineDisolveStrengthCurrent = 10;
    private float shaderTimerCurrent = 0;
    private float forceTimerCurrent = 70;
    private float spawnRateCurrent = 10;

    private FMOD.Studio.EventInstance Checkpoint;

    // Start is called before the first frame update
    void Start()
    {
        shockwaveShaderMaterial.SetFloat("Size_", 0f);
        shockwaveShaderMaterial.SetFloat("Timer_", 0);
        shineShaderMaterial.SetFloat("DisolveStrength_", 5);
        shineShaderMaterial.SetFloat("MaskPower_", 7);
        shockwaveQuads.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.C)) //Det som triggar hela effekten. Byts ut i riktiga spelet till när spelaren aktiverar checkpointen på något sätt.
        {
            StartEffect();
        }
        */

        CheckPlayShine();
        CheckStopShine();
        CheckPlayShader();
        CheckReduceForce();
        CheckReduceSpawnRate();
    }


    public void StartEffect()
    {
        //Börjar spela shadern och sänka elden
        playShine = true;
        shinePlanes.SetActive(true);

        Checkpoint = FMODUnity.RuntimeManager.CreateInstance("event:/Game/Checkpoint");
        Checkpoint.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        Checkpoint.start();
        Checkpoint.release();

        StartCoroutine(StartShockwave());

    }

    private void CheckPlayShine()
    {
        if (playShine)
        {
            shineDisolveStrengthCurrent -= Time.deltaTime * 6f;
            shineShaderMaterial.SetFloat("DisolveStrength_", shineDisolveStrengthCurrent);
            if (shineDisolveStrengthCurrent <= 0.8f)
            {
                shineDisolveStrengthCurrent = 0.8f;
            }

            shineScaleCurrent += Time.deltaTime * 1.5f;
            shinePlanes.transform.localScale = new Vector3(1, shineScaleCurrent, 1);
            shinePositionCurrent -= Time.deltaTime * 0.5f;
            shinePlanes.transform.position = new Vector3(transform.position.x, shinePositionCurrent, transform.position.z);

            shineMaskPowerCurrent -= Time.deltaTime * 6f;
            shineShaderMaterial.SetFloat("MaskPower_", shineMaskPowerCurrent);
        }
    }

    private void CheckStopShine()
    {
        if (stopShine)
        {

            shineDisolveStrengthCurrent += Time.deltaTime * 5f;
            if (shineDisolveStrengthCurrent >= 5.62f)
            {
                shineDisolveStrengthCurrent = 5.62f;
            }
            shineShaderMaterial.SetFloat("DisolveStrength_", shineDisolveStrengthCurrent);

            shineScaleCurrent -= Time.deltaTime * 4f;
            if (shineScaleCurrent <= 1.5f)
            {
                shineScaleCurrent = 1.5f;
            }
            shinePlanes.transform.localScale = new Vector3(1, shineScaleCurrent, 1);

            shinePositionCurrent += Time.deltaTime * 1.1f;
            shinePlanes.transform.position = new Vector3(transform.position.x, shinePositionCurrent, transform.position.z);

            shineMaskPowerCurrent += Time.deltaTime * 4f;
            if (shineMaskPowerCurrent >= 2.23f)
            {
                shineMaskPowerCurrent = 2.23f;
            }
            shineShaderMaterial.SetFloat("MaskPower_", shineMaskPowerCurrent);
            
            if (shineMaskPowerCurrent == 2.23f && shineDisolveStrengthCurrent == 5.62f)
            {
                stopShine = false;
            }
        }
    }

    private void CheckPlayShader()
    {
        if (playShader)
        {
            //"Spelar" shadern genom att manuellt öka dess storlek över tid
            shaderTimerCurrent += Time.deltaTime * 0.4f;
            shockwaveShaderMaterial.SetFloat("Timer_", shaderTimerCurrent);

            //"Stänger av" shadern när den når sin maximala storlek (1)
            if (shaderTimerCurrent >= 1)
            {
                shockwaveShaderMaterial.SetFloat("Size_", 0f);
                playShader = false;
            }
        }
    }

    private void CheckReduceForce()
    {
        if (reduceForce)
        {
            //Stänger av kraften på partiklarna över tid för att få en mer "smooth" nedgång
            forceTimerCurrent -= Time.deltaTime * 100f;
            if (forceTimerCurrent <= 0)
            {
                forceTimerCurrent = 0;
                reduceForce = false;
            }

            for (int i = 0; i < checkpointGraphs.Length; i++)
            {
                checkpointGraphs[i].SetVector3("ExplosionForce", new Vector3(forceTimerCurrent, forceTimerCurrent, forceTimerCurrent));
            }
        }
    }

    private void CheckReduceSpawnRate()
    {
        if (reduceSpawnRate)
        {
            //Stänger av spawn rate över tid för att elden ska vara så utspridd medan kraften verkar på den
            spawnRateCurrent -= Time.deltaTime * 7f;
            if (spawnRateCurrent <= 1)
            {
                spawnRateCurrent = 1;
                reduceSpawnRate = false;
            }

            for (int i = 0; i < checkpointGraphs.Length; i++)
            {
                checkpointGraphs[i].SetFloat("SpawnRateMultiplier", spawnRateCurrent);
            }
        }
    }

    IEnumerator StartShockwave()
    {
        yield return new WaitForSeconds(1.7f);

        playShine = false;
        stopShine = true;

        playShader = true;
        reduceForce = true;
        reduceSpawnRate = true;
        shockwaveShaderMaterial.SetFloat("Size_", 0.07f);

        for (int i = 0; i < checkpointGraphs.Length; i++)
        {
            switch (ParticleDensityScript.particleDensitySetting)
            {
                case 1:
                    checkpointGraphs[i].SendEvent("TriggerExplosion");
                    break;
                case 2:
                    checkpointGraphs[i].SendEvent("TriggerExplosionReduced");
                    break;
                case 3:
                    checkpointGraphs[i].SendEvent("TriggerExplosionMinimal");
                    break;
                default:
                    checkpointGraphs[i].SendEvent("TriggerExplosion");
                    break;

            }
            checkpointGraphs[i].SendEvent("TriggerExplosion");
        }

        StartCoroutine(ResetFire());
    }

    IEnumerator ResetFire()
    {   
        //Resetar lifetimen på elden efter shockwaven förtsvunnit så att elden inte verkar stå stilla
        yield return new WaitForSeconds(0.5f);
        
        for(int i = 0; i < checkpointGraphs.Length; i++)
        {
            checkpointGraphs[i].SetVector2("BaseFlameLifetime", new Vector2(3, 2));
            checkpointGraphs[i].SetVector2("WhiteFlameLifetime", new Vector2(1.3f, 1.8f));
            checkpointGraphs[i].SetVector2("FlameGlowLifetime", new Vector2(3f, 4f));
            checkpointGraphs[i].SetVector2("SparkLifetime", new Vector2(3f, 4f));
        }
    }
}
