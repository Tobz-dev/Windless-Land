using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Main Author: Tobias Martinsson
public class FadeToBlack : MonoBehaviour
{
    [SerializeField]
    Color objectColor;
    [SerializeField]
    float fadeSpeed = 0.35f;

    //Calls coroutine to activate the blackscreen fade-in
    public void ActivateBlackScreen()
    {
        
        StartCoroutine(FadeOut());
    }
    //Calls coroutine to activate the blackscreen fade-out
    public void DisableBlackScreen()
    {

        StartCoroutine(FadeIn());
    }

    //Fades out the screen, aka fades in a black-screen. 
    private IEnumerator FadeOut()
    {
    
        objectColor = gameObject.GetComponent<Image>().color;
        float fadeAmount;
        while (gameObject.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            gameObject.GetComponent<Image>().color = objectColor;
            //Debug.Log("Faded to black");
            yield return null;
        }
        //StartCoroutine(FadeIn());

    }
    //Fades in the normal screen, aka fades out the blackscreen.
    private IEnumerator FadeIn()
    {

        objectColor = gameObject.GetComponent<Image>().color;
        float fadeAmount;

        while (gameObject.GetComponent<Image>().color.a > 0)
        {
            fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            gameObject.GetComponent<Image>().color = objectColor;
            //Debug.Log("Faded to transparent");
            yield return null;
        }
        gameObject.SetActive(false);
    }
}

