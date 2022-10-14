using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChoicePortalTrigger : MonoBehaviour
{
    [SerializeField]
    private string triggerName;
    //a reference to the non-adjecent portal.
    [SerializeField]
    private GameObject nonadjecentPortal;

    [SerializeField]
    private string levelToLoad;

    [SerializeField]
    private GameObject CPTParent;

    [SerializeField]
    private GameObject soulBeforePortal;

    [SerializeField]
    private GameObject soulAfterPortal;


    // Start is called before the first frame update
    private void Start()
    {
        if (PlayerPrefs.GetString(triggerName).Equals("activated"))
        {
            Debug.Log("in portal trigger. " + triggerName + "was active");
            ActiavteNewSoul();
            ChangeLevelOfPortal();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerPrefs.SetString(triggerName, "NotActivated");
            Debug.Log("in portal trigger. pressed O");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("in portal trigger. Player entered.  ");
            ChangeLevelOfPortal();

            //save which portal was activated.
            PlayerPrefs.SetString(triggerName, "activated");
        }
    }

    private void ChangeLevelOfPortal() 
    {
        nonadjecentPortal.GetComponent<NextSceneScript>().ChangeNameOfLevelToLoad(levelToLoad);

        CPTParent.SetActive(false);
        Debug.Log("in portal trigger. ChangeLevelOfPortal");
    }

    //easy way to activate the soul with dialogue based on the player dying to the boss.
    //and since it checks on scene load, the player can't go back and see the new dialogue before they should.
    private void ActiavteNewSoul() 
    {
        soulBeforePortal.SetActive(false);
        soulAfterPortal.SetActive(true);
    }
}
