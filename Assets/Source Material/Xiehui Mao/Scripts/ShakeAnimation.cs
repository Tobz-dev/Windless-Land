using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeAnimation : MonoBehaviour
{
    //Referens: https://forum.unity.com/threads/how-to-activate-a-child-of-a-parent-object.378133/

    [SerializeField] private Animator shakeAnimationController;


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            shakeAnimationController.SetBool("Shake", true);
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            shakeAnimationController.SetBool("Shake", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shakeAnimationController.SetBool("Shake", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shakeAnimationController.SetBool("Shake", false);
        }
    }
}
