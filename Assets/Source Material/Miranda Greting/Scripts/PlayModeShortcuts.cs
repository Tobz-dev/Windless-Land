using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.ShortcutManagement;

[InitializeOnLoad]
public static class EnterPlayModeBindings
{
    //inactivates unity editor shortcuts in playmode
    //requires 1. going to Edit > Shortcuts. 2. creating a new shortcut profile named "Play". 3. Disabling the editor shortcuts you don't want during playmode (e.g. ctrl + z)
    /*
    static EnterPlayModeBindings()
    {
        EditorApplication.playModeStateChanged += ModeChanged;
        EditorApplication.quitting += Quitting;
    }

    static void ModeChanged(PlayModeStateChange playModeState)
    {
        if (playModeState == PlayModeStateChange.EnteredPlayMode)
            ShortcutManager.instance.activeProfileId = "Play";
        else if (playModeState == PlayModeStateChange.EnteredEditMode)
            ShortcutManager.instance.activeProfileId = "Default";
    }

    static void Quitting()
    {
        ShortcutManager.instance.activeProfileId = "Default";
    }
    */
}
