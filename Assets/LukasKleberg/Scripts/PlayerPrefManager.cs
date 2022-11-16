using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefManager : MonoBehaviour
{

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

        //something for dialogue?
        PlayerPrefs.SetString("FirstDialogueHasBeenActivated", "False");

        //latest scene loaded
        PlayerPrefs.SetString("LatestSceneLoadedPref", "blank");
    }

    public void SetPotionAmount(int amount) 
    {
        PlayerPrefs.SetInt("PotionAmountPref", amount);
    }
}
