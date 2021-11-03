using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;
    [SerializeField]
    private GameObject pauseMenuBackground;
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject eventSystemHelper;


    private void Start()
    {
        if (gameIsPaused)
            Pause();
        else
            Resume();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO this via events so it can be remapped
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else 
            {
                Pause();
            }
        }

    }

    public void Resume() 
    {
        pauseMenuBackground.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;
        eventSystemHelper.SetActive(false);
    }

    public void Pause()
    {
        pauseMenuBackground.SetActive(true);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        gameIsPaused = true;
        eventSystemHelper.SetActive(true);
    }

    public void LoadScene(string sceneToLoad) 
    {
        Debug.Log("in PauseMenu, LoadScene with" + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
