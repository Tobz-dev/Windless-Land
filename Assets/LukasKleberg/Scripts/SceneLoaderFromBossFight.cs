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
        latestSceneLoaded = PlayerPrefs.GetString("LatestSceneLoadedPref");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealthScript.health < 1) 
        {
            //gives a delay so the player controller can fade the screen to black.
            StartCoroutine(SceneLoadDelay());
                
        }

        if (!GameObject.Find("Boss")) 
        {
            LoadCreditsScene();
        }
    }

    private IEnumerator SceneLoadDelay() 
    {
        yield return new WaitForSeconds(4.0f);
        LoadSceneBasedOnLatestScene();
    }

    private void LoadSceneBasedOnLatestScene() 
    {
        switch (latestSceneLoaded)
        {
            case "Level1_V2.0":
                Debug.Log("in SceneFromBoss: switch. was Lv1");
                PlayerPrefs.SetString("LatestSceneLoadedPref", SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("Level1_V2.0");
                break;
            /*case "Level2_V3.0":
                Debug.Log("in SceneFromBoss: switch. was Lv1");
                PlayerPrefs.SetString("LatestSceneLoadedPref", SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("Level2_V3.0");
                break;
                */
            /*case "Level3_V3.0":
                Debug.Log("in SceneFromBoss: switch. was Lv2");
                PlayerPrefs.SetString("LatestSceneLoadedPref", SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("Level3_V3.0");
                break;
            */
            default:
                Debug.Log("in SceneFromBoss: switch. was default");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }
    }

    private void LoadCreditsScene() 
    {
        SceneManager.LoadScene("CreditsScene_V1.0");
    }
}
