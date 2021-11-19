using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMechanicsScript : MonoBehaviour
{

    public GameObject rightFloor, leftFloor;
    public float aggroRange;
    public GameObject fallingPlatform1, fallingPlatform2, fallingPlatform3;
    public Transform adSpawnPoint1, adSpawnPoint2;
    public GameObject attackHitboxRight;
    public GameObject attackHitboxLeft;
    public List<GameObject> PillarList;

    // Start is called before the first frame update

    public void FadeIn(GameObject floor) 
    {
        StartCoroutine(FadeTo(1.0f, 2.0f, floor));
    }
   

    IEnumerator FadeTo(float aValue, float aTime, GameObject floor)
    {
        float alpha = floor.gameObject.GetComponent<MeshRenderer>().material.color.a;
        for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = floor.gameObject.GetComponent<MeshRenderer>().material.color;
            newColor.a = Mathf.Lerp(alpha, aValue, t);
            floor.gameObject.GetComponent<MeshRenderer>().material.color = newColor;

            yield return null;
        }
        //Debug.Log("Faded in");

        if(floor.name == "BossDangerFloorRight")
        {
            Instantiate(attackHitboxRight);
        }
        else if(floor.name == "BossDangerFloorLeft")
        {
           Instantiate(attackHitboxLeft);
        }

        StartCoroutine(FadeOut(0.0f, aTime / 4, floor));
    }

    IEnumerator FadeOut(float aValue, float aTime, GameObject floor)
    {
        float alpha = floor.gameObject.GetComponent<MeshRenderer>().material.color.a;
        for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = floor.gameObject.GetComponent<MeshRenderer>().material.color;
            newColor.a = Mathf.Lerp(alpha, aValue, t);
            floor.gameObject.GetComponent<MeshRenderer>().material.color = newColor;

            yield return null;
        }
        //Debug.Log("Faded out");
    }

    public void DestroyRandomPillar()
    {
        GameObject go = PillarList[Random.Range(0, PillarList.Count)];
        Destroy(go);
        PillarList.Remove(go);
    }
}
