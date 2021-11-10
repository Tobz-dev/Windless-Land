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
}
