using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenAnim : MonoBehaviour
{

    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void openDoor()
    {
        anim.SetBool("Open", true);
    }


}
