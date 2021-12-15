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
    private GameObject playerHUD;

    [SerializeField]
    private GameObject eventSystemHelper;

    [SerializeField]
    private GameObject[] subMenus;

    [SerializeField]
    private GameObject prototypeController => GameObject.Find("TobiasPrototypeController");

    [SerializeField] private GameObject logpanel;
    private bool logIsUp;

    private void Start()
    {
        checkIfLog();
        if (logpanel != null)
        {
            logpanel.SetActive(false);
        }
        
        gameIsPaused = false;

        eventSystemHelper.SetActive(false);
        pauseMenuBackground.SetActive(false);
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

        if(logpanel == isActiveAndEnabled && Input.GetKeyDown(KeyCode.LeftControl))
        {
            logpanel.SetActive(false);
        }


    }

    public void Resume() 
    {
        
        pauseMenuBackground.SetActive(false);
        pauseMenuUI.SetActive(false);
        eventSystemHelper.GetComponent<EventSystemHelper>().UnlockMouseCursor();
        eventSystemHelper.SetActive(false);

        playerHUD.SetActive(true);

        if (prototypeController.GetComponent<PrototypeScript>().prototypeEnabled == true)
        {

            Time.timeScale = prototypeController.GetComponent<PrototypeScript>().timeScaleVariable;
            //Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            Time.timeScale = 1f;
        }
        gameIsPaused = false;

        //Debug.Log("in PauseMenu. resume");
        for (int i = 0; i <= subMenus.Length - 1; i++)
        {
            subMenus[i].SetActive(false);   
        }
    }

    public void Pause()
    {
        pauseMenuBackground.SetActive(true);
        pauseMenuUI.SetActive(true);
        eventSystemHelper.SetActive(true);

        playerHUD.SetActive(false);

        Time.timeScale = 0.0f;
        gameIsPaused = true;
    }

    public void LoadScene(string sceneToLoad) 
    {
        Debug.Log("in PauseMenu, LoadScene with" + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void checkIfLog()
    {
        if(logpanel == isActiveAndEnabled)
        {
            logIsUp = true;
        }
        else
        {
            logIsUp = false;
        }
    }



}
