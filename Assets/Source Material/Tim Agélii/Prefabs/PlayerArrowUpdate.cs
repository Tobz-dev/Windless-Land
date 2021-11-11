using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowUpdate : MonoBehaviour
{
    private Rigidbody ArrowRgb;

    private float speed = 25f;
    // Start is called before the first frame update
    void Start()
    {
      ArrowRgb =  transform.GetComponent<Rigidbody>();
        ArrowRgb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
 
}
