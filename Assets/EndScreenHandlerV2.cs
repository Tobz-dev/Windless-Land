using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenHandlerV2 : MonoBehaviour
{
    [SerializeField]
    GameObject fadeToBlack;
    [SerializeField]
    GameObject bossHud;
    [SerializeField]
    GameObject secondFadeToBlack;

    [SerializeField]
    GameObject endText;

    Color objectColor;
    float fadeSpeed = 0.25f;


    bool faded = false;
    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("Boss") && faded == false)
        {
            fadeToBlack.SetActive(true);
            fadeToBlack.GetComponent<FadeToBlack>().ActivateBlackScreen();
            faded = true;
            bossHud.SetActive(false);
            Debug.Log("memes!!!");

            StartCoroutine(FadeInText());
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeInText()
    {
        yield return new WaitForSeconds(3f);
        endText.SetActive(true);
    }



    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(15f);
        objectColor = secondFadeToBlack.GetComponent<Image>().color;
        float fadeAmount;
        while (secondFadeToBlack.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            secondFadeToBlack.GetComponent<Image>().color = objectColor;
            //Debug.Log("Faded to black");
            yield return null;
        }
        SceneManager.LoadScene("MainMenu");

    }
}
