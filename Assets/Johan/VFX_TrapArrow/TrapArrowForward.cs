using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Main Author: Johan Nydahl

public class TrapArrowForward : MonoBehaviour
{
    private Vector3 startPos;
    private float resetPos;



    private void Awake()
    {
        startPos = transform.position;
        resetPos = 18;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x >= resetPos)
        {
            transform.position = startPos;
        }
        else
        {
            transform.position -= transform.forward * 5 * Time.deltaTime;
        }
    }
}
