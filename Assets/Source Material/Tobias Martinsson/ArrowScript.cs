using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowSpawnPosition;
    public float shootForce = 10f;
    public float arrowLifeTime;



    // Update is called once per frame
    void Update()
    {
    }

    public void shootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPosition.position, arrowSpawnPosition.rotation);

        arrow.GetComponent<Rigidbody>().velocity = arrowSpawnPosition.transform.forward * shootForce;

        Destroy(arrow, arrowLifeTime);
    }
}
