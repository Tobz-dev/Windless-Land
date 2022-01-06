using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VCAController : MonoBehaviour
{

    private FMOD.Studio.VCA VcaController;
    public string VcaName;

    [SerializeField] private float vcaVolume;

    private Slider slider;

    private float savedVolumeSetting;
    private bool isCurrentlyMuted;
    public Image checkBoxImage;

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
        SetToSavedValues();
    }

    public void SetVolume(float volume)
    {
        VcaController.setVolume(volume);
        VcaController.getVolume(out vcaVolume);
        //Debug.Log("Setvolume, vcaVolume is:" + vcaVolume);
        PlayerPrefs.SetFloat(VcaName, vcaVolume);


    }

    //the settings need to be applied when the player starts the game. 
    //so call this method. If the applying is in start it only applies when the sliders are set to active. 
    public void SetToSavedValues() 
    {
        VcaController = FMODUnity.RuntimeManager.GetVCA("vca:/" + VcaName);
        slider = GetComponent<Slider>();
        VcaController.getVolume(out vcaVolume);

        slider.value = PlayerPrefs.GetFloat(VcaName, 1.0f);

        string mutedString = PlayerPrefs.GetString(VcaName + "IsMuted", "NotMuted");

        if (mutedString.Equals("Muted"))
        {
            Debug.Log("vca controller. string was muted");
            checkBoxImage.enabled = true;
            isCurrentlyMuted = true;
        }
    }

    public void AudioToggle()
    {
        //this could use vcaVolume but I'm a bit unsure of how it works. -Lukas
        //if it is currently muted, then it unmutes by setting to the saved value.
        Debug.Log("in audiotoggle. currently muted is: " + isCurrentlyMuted);
        if (isCurrentlyMuted)
        {
            slider.value = PlayerPrefs.GetFloat(VcaName + "SavedValue");

            isCurrentlyMuted = !isCurrentlyMuted;
            //set string to NotMuted
            PlayerPrefs.SetString(VcaName + "IsMuted", "NotMuted");
        }
        //if it is currently not muted, it saves the current value and then mutes.
        else
        {
            PlayerPrefs.SetFloat(VcaName + "SavedValue", slider.value);

            slider.value = 0.0f;
            isCurrentlyMuted = !isCurrentlyMuted;
            //set string to Muted
            PlayerPrefs.SetString(VcaName + "IsMuted", "Muted");
        }
    }

    public void PlayPlayerSFXSound()
    {
        if (FmodExtensions.IsPlaying(PlayerSFX))
        {
        }
        else
        {
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
            UI = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Yes1");
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
