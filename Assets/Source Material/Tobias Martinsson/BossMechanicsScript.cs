using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tobias Martinsson

public class BossMechanicsScript : MonoBehaviour
{

    [SerializeField]
    public GameObject rightFloor, leftFloor;
    [SerializeField]
    private float aggroRange;
    [SerializeField]
    public GameObject fallingPlatform1, fallingPlatform2, fallingPlatform3;
    [SerializeField]
    public Transform adSpawnPoint1, adSpawnPoint2;
    [SerializeField]
    private GameObject attackHitboxRight;
    [SerializeField]
    private GameObject attackHitboxLeft;
    [SerializeField]
    private List<GameObject> PillarList;
    [SerializeField]
    private List<GameObject> tempPillarList;
    [SerializeField]
    private GameObject circleVisibleHitBox;
    [SerializeField]
    private GameObject circleHitBox;
    [SerializeField]
    private GameObject circleVFX;

    [SerializeField]
    private Vector3 playPosAtAttack;

    private void Start()
    {
        tempPillarList = new List<GameObject>(PillarList);
    }

    // Fades in the floor-attack.
    public void FadeIn(GameObject floor) 
    {
        StartCoroutine(FadeTo(1.0f, 2.0f, floor));
    }

    //Decides position of the circle hitbox, and calls the coroutine.
    public void FadeInCircle()
    {
        playPosAtAttack = GameObject.FindGameObjectWithTag("Player").transform.position;
        circleVisibleHitBox.transform.position = playPosAtAttack;
        StartCoroutine(FadeTo(1.0f, 2.0f, circleVisibleHitBox));
    }

    

    //Fades in the attack.
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
        
        //Decides what to spawn, based on the name of the object sent as a parameter.

        if(floor.name == "BossDangerFloorRight")
        {
            Instantiate(attackHitboxRight);
        }
        else if(floor.name == "BossDangerFloorLeft")
        {
           Instantiate(attackHitboxLeft);
        }
        else if(floor.name == "BossDangerFloorCircle")
        {
            GameObject memes = Instantiate(circleHitBox, playPosAtAttack, Quaternion.Euler(0,0,0));



            float tempZPosition = playPosAtAttack.z - 4;
            //tempPositionHolder.transform.position.z = 
            Vector3 tempVFXPosition = new Vector3(playPosAtAttack.x, playPosAtAttack.y, tempZPosition);

            Instantiate(circleVFX, tempVFXPosition, Quaternion.Euler(0, 0, 0));

        }
        //Starts a coroutine to fadeout the attack after it has fully been faded in.
        StartCoroutine(FadeOut(0.0f, aTime / 4, floor));
    }

    //Fades out attack.
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

    //Respawns the pillars, called when the player dies and the boss resets. 
    public void RespawnPillars()
    {
        tempPillarList = new List<GameObject>(PillarList);
        foreach (GameObject go in PillarList)
        {
            go.GetComponent<MeshRenderer>().enabled = true;
            go.GetComponent<BoxCollider>().enabled = true;
        }
        
    }

    //Getter for a random pillar that the boss destroys, made sure not to pick an already deleted pillar.
    public Transform GetRandomPillar()
    {
        GameObject go = tempPillarList[Random.Range(0, tempPillarList.Count)];
        tempPillarList.Remove(go);
        return go.transform;
    } 

    //Destroys the random pillar and removes it from the array, so it cannot be picked again.
    public void DestroyRandomPillar()
    {

        GameObject go = tempPillarList[Random.Range(0, tempPillarList.Count)];

        go.GetComponent<MeshRenderer>().enabled = false;
        go.GetComponent<BoxCollider>().enabled = false;
    }
}
