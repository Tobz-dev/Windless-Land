using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Secondary Author: William Smith
public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private FMOD.Studio.EventInstance PortalEnter;
    public Transform checkpiont;
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PortalEnter = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/PortalEnter");
            PortalEnter.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            PortalEnter.start();
            PortalEnter.release();
            Invoke("Shortcut", 2);
        }
    }

   void Shortcut()
    {
        player.transform.position = checkpiont.position;
        player.transform.rotation = checkpiont.rotation;
    }
}
