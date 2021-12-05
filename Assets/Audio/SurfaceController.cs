using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceController : MonoBehaviour
{

    private enum CURRENT_TERRAIN { GRASS, GRAVEL, WOOD, STONE, DIRT };

    [SerializeField]
    private CURRENT_TERRAIN currentTerrain;

    private FMOD.Studio.EventInstance foosteps;
    private FMOD.Studio.EventInstance EnemyFootsteps;
    private FMOD.Studio.EventInstance EnemyAttack;
    private FMOD.Studio.EventInstance BowDraw;
    private FMOD.Studio.EventInstance PlayerAttack;
    private FMOD.Studio.EventInstance BowShot;
    private FMOD.Studio.EventInstance PlayerAttackStrong;

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
        PlayerAttack = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Player/Attack");
        PlayerAttack.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        PlayerAttack.start();
        PlayerAttack.release();

    }

    public void SelectAndPlayEnemyFootstep()
    {
        switch (currentTerrain)
        {
            case CURRENT_TERRAIN.GRAVEL:
                PlayEnemyFootstep(1);
                break;

            case CURRENT_TERRAIN.GRASS:
                PlayEnemyFootstep(0);
                break;

            case CURRENT_TERRAIN.WOOD:
                PlayEnemyFootstep(2);
                break;

            case CURRENT_TERRAIN.STONE:
                PlayEnemyFootstep(3);
                break;

            case CURRENT_TERRAIN.DIRT:
                PlayEnemyFootstep(4);
                break;

            default:
                PlayEnemyFootstep(0);
                break;
        }
    }

    private void PlayEnemyFootstep(int terrain)
    {
        EnemyFootsteps = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Small Enemy/SmallEnemyFootsteps");
        EnemyFootsteps.setParameterByName("Terrain", terrain);
        EnemyFootsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        EnemyFootsteps.start();
        EnemyFootsteps.release();
    }

    public void PlayEnemyAttack()
    {
        EnemyAttack = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Small Enemy/SmallEnemyAttack");
        EnemyAttack.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        EnemyAttack.start();
        EnemyAttack.release();

    }

    public void PlayBowDraw()
    {
        BowDraw = FMODUnity.RuntimeManager.CreateInstance("event:/Objects/BowDraw");
        BowDraw.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        BowDraw.start();
        BowDraw.release();

    }

    public void PlayBowShot()
    {
        BowShot = FMODUnity.RuntimeManager.CreateInstance("event:/Objects/ArrowShot");
        BowShot.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        BowShot.start();
        BowShot.release();

    }

    public void PlayPlayerAttackStrong()
    {
        PlayerAttackStrong = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Player/AttackStrong");
        PlayerAttackStrong.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        PlayerAttackStrong.start();
        PlayerAttackStrong.release();

    }

}

