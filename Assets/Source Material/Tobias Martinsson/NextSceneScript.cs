using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Main Author: Tobias Martinsson
public class NextSceneScript : MonoBehaviour
{
    [SerializeField]
    private string nameOfLevelToLoad;
    private FMOD.Studio.EventInstance PortalEnter;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PortalEnter = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/PortalEnter");
            PortalEnter.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            PortalEnter.start();
            PortalEnter.release();
            SceneManager.LoadScene(nameOfLevelToLoad);
        }
    }
}
