using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMechanicsScript : MonoBehaviour
{

    public GameObject rightFloor, leftFloor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fadeIn(GameObject floor)
    {
        StartCoroutine(FadeTo(1.0f, 1.0f, floor));
    }

    public void fadeOut(GameObject floor) 
    {
        StartCoroutine(FadeTo(0.0f, 1.0f, floor));
    }

    IEnumerator FadeTo(float aValue, float aTime, GameObject floor)
    {
        float alpha = floor.gameObject.GetComponent<MeshRenderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = floor.gameObject.GetComponent<MeshRenderer>().material.color;
            newColor.a = Mathf.Lerp(alpha, aValue, t);
            floor.gameObject.GetComponent<MeshRenderer>().material.color = newColor;
            yield return null;
        }
    }
}
