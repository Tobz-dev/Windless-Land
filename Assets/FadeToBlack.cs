using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeToBlack : MonoBehaviour
{
    Color objectColor;
    // Start is called before the first frame update
    float fadeSpeed = 0.2f;

    public void ActivateBlackScreen()
    {
        
        StartCoroutine(FadeOut());
    }

    public void DisableBlackScreen()
    {

        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
    
        objectColor = gameObject.GetComponent<Image>().color;
        float fadeAmount;
        while (gameObject.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            gameObject.GetComponent<Image>().color = objectColor;
            Debug.Log("Faded to black");
            yield return null;
        }
        //StartCoroutine(FadeIn());

    }

    private IEnumerator FadeIn()
    {

        objectColor = gameObject.GetComponent<Image>().color;
        float fadeAmount;

        while (gameObject.GetComponent<Image>().color.a > 0)
        {
            fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            gameObject.GetComponent<Image>().color = objectColor;
            Debug.Log("Faded to black");
            yield return null;
        }
    }
}

