using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VCAController : MonoBehaviour
{

    private FMOD.Studio.VCA VcaController;
    public string VcaName;
    private FMOD.Studio.EventInstance PlatformFalling;
    private FMOD.Studio.EventInstance PlayerSFX;
    private FMOD.Studio.EventInstance EnemySFX;
    private FMOD.Studio.EventInstance SFX;
    private FMOD.Studio.EventInstance Ambience; 
    private FMOD.Studio.EventInstance Music;
    private FMOD.Studio.EventInstance UI;
    private FMOD.Studio.EventInstance Master;
    private FMOD.Studio.EventInstance safeRoom;
    private bool isPlaying;
    public GameObject Attenuation;

    

    [SerializeField] private float vcaVolume;

    private Slider slider;


    public class FmodExtensions
    {
        public static bool IsPlaying(FMOD.Studio.EventInstance instance)
        {
            FMOD.Studio.PLAYBACK_STATE state;
            instance.getPlaybackState(out state);
            return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        VcaController = FMODUnity.RuntimeManager.GetVCA("vca:/" + VcaName);
        slider = GetComponent<Slider>();
        VcaController.getVolume(out vcaVolume);
    }

    public void SetVolume(float volume)
    {
        VcaController.setVolume(volume);
        VcaController.getVolume(out vcaVolume);
        

    }

    public void SetMusicParameter(float volume)
    {



    }

    public void SetSpatialization(float volume)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SWidth", volume);


    }

    public void PlayPlayerSFXSound()
    {
        if (FmodExtensions.IsPlaying(PlayerSFX)) {
        }
        else { 
        PlayerSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Player/Attack");
        PlayerSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Attenuation));
        PlayerSFX.start();
        PlayerSFX.release();
        }
    }

    public void PlayEnemySFXSound()
    {
        if (FmodExtensions.IsPlaying(EnemySFX))
        {
        }
        else
        {
            EnemySFX = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Small Enemy/SmallEnemyAttack");
            EnemySFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Attenuation));
            EnemySFX.start();
            EnemySFX.release();
        }
    }

    public void PlaySFXSound()
    {
        if (FmodExtensions.IsPlaying(SFX))
        {
        }
        else
        {
            SFX = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/KeyPickUp");
            SFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Attenuation));
            SFX.start();
            SFX.release();
        }
    }

    public void PlayAmbienceSound()
    {
        if (FmodExtensions.IsPlaying(Ambience))
        {
        }
        else
        {
            Ambience = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/AmbienceExample");
            Ambience.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Attenuation));
            Ambience.start();
            Ambience.release();
        }
    }

    public void PlayMusicSound()
    {
        if (FmodExtensions.IsPlaying(Music))
        {
        }
        else
        {
            Music = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Player/Footsteps");
            Music.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Attenuation));
            Music.start();
            Music.release();
        }
    }

    public void PlayUISound()
    {
        if (FmodExtensions.IsPlaying(UI))
        {
        }
        else
        {
            UI = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Yes");
            UI.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Attenuation));
            UI.start();
            UI.release();
        }
    }

    public void PlayMasterSound()
    {
        if (FmodExtensions.IsPlaying(Master))
        {
        }
        else
        {
            Master = FMODUnity.RuntimeManager.CreateInstance("event:/Game/SwordImpact");
            Master.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Attenuation));
            Master.start();
            Master.release();
        }
    }

}
