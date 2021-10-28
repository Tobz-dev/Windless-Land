using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeController : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("TEST P KEY");
            //GameObject.Find("other_object_name").GetComponent(B).enabled = false;
            //GameObject.Find("other_object_name").GetComponent(B).enabled = false;
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("UpArrow");
            gameObject.GetComponent<CharacterController>().enabled = false;
            gameObject.GetComponent<PrototypeCharacterController>().enabled = true;


        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("DownArrow");
            gameObject.GetComponent<CharacterController>().enabled = true;
            gameObject.GetComponent<PrototypeCharacterController>().enabled = false;

        }


        //SceneManager.LoadScene("HenrikPrototyp");
    }
}
