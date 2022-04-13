using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Henrik Ruden

public class UIFaceCamera : MonoBehaviour
{

    private Transform objectToFace;

    // Start is called before the first frame update
    void Start()
    {
        objectToFace = GameObject.FindGameObjectWithTag("MiddleOfCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(objectToFace);
    }
}
