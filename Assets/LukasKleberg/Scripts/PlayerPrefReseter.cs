using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefReseter : MonoBehaviour
{

    //resets all PlayerPrefs
    public void ResetPlayerPrefs() 
    {
        //Level1 Portal Triggers
        PlayerPrefs.SetString("TriggerA", "NotActivated");
        PlayerPrefs.SetString("TriggerB", "NotActivated");

        //health potions gathered

        //latest scene loaded
        PlayerPrefs.SetString("latestSceneLoaded", "blank");
    }
}
