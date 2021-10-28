using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeScript : MonoBehaviour
{
    public GameObject smallEnemy;
    public GameObject bigEnemy;
    public GameObject rangedEnemy;
    public float slowRate;

    private bool slowMotion = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Instantiate(smallEnemy);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Instantiate(bigEnemy);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Instantiate(rangedEnemy);

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(slowMotion == false)
            {
                Debug.Log("slowmotion");
                Time.timeScale = 0.5f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                slowMotion = true;
            }
            else
            {
                Debug.Log("slowmotion");
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                slowMotion = false;
            }
            
        }
        
    }

    public void ChangeTimeScale(float x)
    {
        Time.timeScale = x;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}
