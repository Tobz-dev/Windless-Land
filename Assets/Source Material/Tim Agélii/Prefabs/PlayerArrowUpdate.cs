using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Main Author: Tim Agelii

public class PlayerArrowUpdate : MonoBehaviour
{
    private Rigidbody ArrowRgb;

    private float speed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        ArrowRgb = transform.GetComponent<Rigidbody>();
        ArrowRgb.velocity = transform.forward * speed;
    }

    public void StopArrow()
    {
        speed = 0f;
        ArrowRgb.velocity = transform.forward * speed;
    }



}
