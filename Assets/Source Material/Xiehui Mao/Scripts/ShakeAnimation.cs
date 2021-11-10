using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeAnimation : MonoBehaviour
{
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
}
