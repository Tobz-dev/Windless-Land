using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorAnimationScript : MonoBehaviour
{
    [SerializeField]
    private bool playAnimation = true;
    [SerializeField]
    private Animator animator;
    private bool isPlaying;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playAnimation)
        {
            StartAnimation();
        }
        animator.Update(Time.deltaTime);
        /*
        if (playAnimation && !isPlaying)
        {
            StartAnimation();
        }
        else if (playAnimation)
        {
            animator.Update(Time.deltaTime);
        }
        else if(!playAnimation && isPlaying)
        {
            StopAnimation();
        }
        */
    }

    private void StartAnimation()
    {
        
        playAnimation = false;
        animator.SetTrigger("BowAim");
        //isPlaying = true;
    }

    private void StopAnimation()
    {
        animator.SetTrigger("StopBow");
        isPlaying = false;
    }
}
