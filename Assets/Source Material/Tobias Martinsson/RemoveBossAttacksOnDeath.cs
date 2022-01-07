using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tobias Martinsson
public class RemoveBossAttacksOnDeath : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(!GameObject.Find("Boss")){
            gameObject.SetActive(false);
        }
    }
}
