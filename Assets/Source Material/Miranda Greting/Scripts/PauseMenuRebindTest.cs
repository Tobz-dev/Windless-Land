using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuRebindTest : MonoBehaviour
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

    [SerializeField]
    private Texture2D cursorTexture;

    [SerializeField] private GameObject logpanel;
    private bool logIsUp;

    PlayerInputs inputActions; //asset with all actions available to player

    private void Awake()
    {
        inputActions = InputManager.inputActions; //links inputActions to the instance created from InputManager
    }

    private void OnEnable()
    {
        inputActions.WindlessLand.Enable();
    }

    private void OnDisable()
    {
        inputActions.WindlessLand.Disable();
    }

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

        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);

    }

    // Update is called once per frame
    void Update()
    {
        //TODO this via events so it can be remapped
        if (/*Input.GetKeyDown(KeyCode.Escape)*/inputActions.WindlessLand.Pause.triggered)
        {
            Debug.Log("pressed");
            if (gameIsPaused)
            {
                Resume();
            }
            else if (logpanel != isActiveAndEnabled) //possible solution to pause bug? only pauses when log != active
            {
                Pause();
            }



        }
        if (logpanel == isActiveAndEnabled && /*Input.GetKeyDown(KeyCode.LeftControl)*/ Input.anyKeyDown) //any keypress inactivates log panel - will players know it's left ctrl?
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
        if (logpanel == isActiveAndEnabled)
        {
            logIsUp = true;
        }
        else
        {
            logIsUp = false;
        }
    }


}
