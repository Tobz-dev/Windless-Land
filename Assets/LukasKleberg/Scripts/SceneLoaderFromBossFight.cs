using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderFromBossFight : MonoBehaviour
{

    //if boss dies, load credits.
    //if the player dies load the latest scene.
    //unless they have made it to boss by clearing all levels.
    //then just reload the scene.

    [SerializeField]
    private PlayerHealthScript playerHealthScript;

    private string latestSceneLoaded;

    // Start is called before the first frame update
    void Start()
    {
        latestSceneLoaded = PlayerPrefs.GetString("latestSceneLoaded");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealthScript.health < 1) 
        {

            LoadSceneBasedOnProgress();
        }

        if (!GameObject.Find("Boss")) 
        {
            LoadCreditsScene();
        }
    }

    private void LoadSceneBasedOnProgress() 
    {
        switch (latestSceneLoaded)
        {
            case "Level1_V2.0":
                SceneManager.LoadScene("Level1_V2.0");
                break;
            case "Level3_V2.0":
                SceneManager.LoadScene("Level3_V2.0");
                break;
            default:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }
    }

    private void LoadCreditsScene() 
    {
        //loads menu as credits scene doesn't exist yet.
        SceneManager.LoadScene("MainMenu");
    }
}
