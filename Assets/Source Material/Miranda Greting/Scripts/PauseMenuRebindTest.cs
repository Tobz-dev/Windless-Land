using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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
    private GameObject prototypeController;

    [SerializeField] private GameObject logpanel;
    private bool logIsUp;

    [SerializeField]
    private Texture2D cursorTexture;

    private PlayerInputs inputActions; //asset with all actions available to player
    private string bindingName;

    private void Awake()
    {
        inputActions = InputManager.inputActions; //links inputActions to the instance created from InputManager
        bindingName = InputManager.GetBindingPath(inputActions.WindlessLand.Pause, 0);
    }

    private void OnEnable()
    {
        inputActions.WindlessLand.Enable();
        inputActions.WindlessLand.Pause.canceled += PausePressed;
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



        if (prototypeController.GetComponent<PrototypeScript>().prototypeEnabled == true)
        {

            Time.timeScale = prototypeController.GetComponent<PrototypeScript>().timeScaleVariable;
            //Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("time scale is " + Time.timeScale);

        //TODO this via events so it can be remapped
        if (/*Input.GetKeyDown(KeyCode.Escape)*/inputActions.WindlessLand.Pause.triggered)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else if (!logIsUp) //possible solution to pause bug? only pauses when log != active
            {
                Pause();
            }
        }

        if (logIsUp && /*Input.GetKeyDown(KeyCode.LeftControl)*/ Input.anyKeyDown) //any keypress inactivates log panel - will players know it's left ctrl?
        {
            logpanel.SetActive(false);
            Debug.Log("LogpanelUp!!");
        }

        if (gameIsPaused && logIsUp)
        {
            inputActions.WindlessLand.Interact.Disable();
            logpanel.SetActive(false);
            Debug.Log("LogpanelUp!!");

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
        eventSystemHelper.GetComponent<EventSystemHelper>().ChangeFirstSelectedObject(pauseMenuUI.transform.GetChild(1).GetChild(0).gameObject);

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
        if (logpanel.activeInHierarchy)
        {
            logIsUp = true;
            Debug.Log("LogpanelUp!!");

        }
        else
        {
            logIsUp = false;
        }
    }

    private void PausePressed(InputAction.CallbackContext ctx)
    {
        if (ctx.control.path.Contains("Gamepad"))
        {
            bindingName = InputManager.GetBindingPath(inputActions.WindlessLand.Pause, 1);
        }
        else
        {
            bindingName = InputManager.GetBindingPath(inputActions.WindlessLand.Pause, 0);
        }
    }

    public bool CheckIfPaused()
    {
        return gameIsPaused;
    }
}
