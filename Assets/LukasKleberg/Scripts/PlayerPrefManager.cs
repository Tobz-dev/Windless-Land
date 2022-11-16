using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefManager : MonoBehaviour
{

    public void OnApplicationQuit()
    {
        //something for dialogue.
        //this is per play-session.
        PlayerPrefs.SetString("FirstDialogueHasBeenActivated", "False");
    }

    //resets all PlayerPrefs
    public void ResetPlayerPrefs() 
    {
        Debug.Log("in PlayerPrefReseter.");

        //Level1 Portal Triggers
        PlayerPrefs.SetString("PortalTriggerAPref", "NotActivated");
        PlayerPrefs.SetString("PortalTriggerBPref", "NotActivated");

        //health potions gathered.
        //remember to add one for each potion.
        PlayerPrefs.SetString("PotionNr1Pref", "notPickedUp");
        PlayerPrefs.SetString("PotionNr2Pref", "notPickedUp");
        PlayerPrefs.SetString("PotionNr3Pref", "notPickedUp");
        PlayerPrefs.SetString("PotionNr4Pref", "notPickedUp");
        PlayerPrefs.SetString("PotionNr5Pref", "notPickedUp");

        //something for dialogue.
        //for when the game is launched the first time. so the PlayerPref should not exist.
        if (!PlayerPrefs.HasKey("FirstDialogueHasBeenActivated")) 
        {
            PlayerPrefs.SetString("FirstDialogueHasBeenActivated", "False");
        }

        //latest scene loaded
        PlayerPrefs.SetString("LatestSceneLoadedPref", "blank");
    }

    public void SetPotionAmount(int amount) 
    {
        PlayerPrefs.SetInt("PotionAmountPref", amount);
    }
}
