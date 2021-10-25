using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceController : MonoBehaviour
{

    private enum CURRENT_TERRAIN { GRASS, GRAVEL, WOOD, STONE, DIRT };

    [SerializeField]
    private CURRENT_TERRAIN currentTerrain;

    private FMOD.Studio.EventInstance foosteps;

    private void Update()
    {
        DetermineTerrain();
    }

    private void DetermineTerrain()
    {
        RaycastHit[] hit;

        hit = Physics.RaycastAll(transform.position, Vector3.down, 10.0f);

        foreach (RaycastHit rayhit in hit)
        {
            if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Gravel"))
            {
                currentTerrain = CURRENT_TERRAIN.GRAVEL;
                break;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Wood"))
            {
                currentTerrain = CURRENT_TERRAIN.WOOD;
                break;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Grass"))
            {
                currentTerrain = CURRENT_TERRAIN.GRASS;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Stone"))
            {
                currentTerrain = CURRENT_TERRAIN.STONE;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Dirt"))
            {
                currentTerrain = CURRENT_TERRAIN.DIRT;
            }
        }
    }

    public void SelectAndPlayFootstep()
    {
        switch (currentTerrain)
        {
            case CURRENT_TERRAIN.GRAVEL:
                PlayFootstep(1);
                break;

            case CURRENT_TERRAIN.GRASS:
                PlayFootstep(0);
                break;

            case CURRENT_TERRAIN.WOOD:
                PlayFootstep(2);
                break;

            case CURRENT_TERRAIN.STONE:
                PlayFootstep(3);
                break;

            case CURRENT_TERRAIN.DIRT:
                PlayFootstep(4);
                break;

            default:
                PlayFootstep(0);
                break;
        }
    }

    private void PlayFootstep(int terrain)
    {
        foosteps = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Player/Footsteps");
        foosteps.setParameterByName("Terrain", terrain);
        foosteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        foosteps.start();
        foosteps.release();
    }

    public void SelectAndPlayRoll()
    {
        switch (currentTerrain)
        {
            case CURRENT_TERRAIN.GRAVEL:
                PlayRoll(1);
                break;

            case CURRENT_TERRAIN.GRASS:
                PlayRoll(0);
                break;

            case CURRENT_TERRAIN.WOOD:
                PlayRoll(2);
                break;

            case CURRENT_TERRAIN.STONE:
                PlayRoll(3);
                break;

            case CURRENT_TERRAIN.DIRT:
                PlayRoll(4);
                break;

            default:
                PlayRoll(0);
                break;
        }
    }

    private void PlayRoll(int terrain)
    {
        foosteps = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Player/Roll");
        foosteps.setParameterByName("Terrain", terrain);
        foosteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        foosteps.start();
        foosteps.release();
    }

    public void SelectAndPlayRollLand()
    {
        switch (currentTerrain)
        {
            case CURRENT_TERRAIN.GRAVEL:
                PlayRollLand(1);
                break;

            case CURRENT_TERRAIN.GRASS:
                PlayRollLand(0);
                break;

            case CURRENT_TERRAIN.WOOD:
                PlayRollLand(2);
                break;

            case CURRENT_TERRAIN.STONE:
                PlayRollLand(3);
                break;

            case CURRENT_TERRAIN.DIRT:
                PlayRollLand(4);
                break;

            default:
                PlayRollLand(0);
                break;
        }
    }

    private void PlayRollLand(int terrain)
    {
        foosteps = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Player/Roll Land");
        foosteps.setParameterByName("Terrain", terrain);
        foosteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        foosteps.start();
        foosteps.release();
    }

    public void PlayAttack()
    {
        foosteps = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Player/Attack");
        foosteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        foosteps.start();
        foosteps.release();

    }

}

