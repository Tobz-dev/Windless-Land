using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowSpawnPosition;
    public float shootForce = 10f;



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            shootArrow();
        }
    }

    public void shootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPosition.position, arrowSpawnPosition.rotation);

        arrow.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * shootForce;

        Destroy(arrow, 5f);
    }
}
