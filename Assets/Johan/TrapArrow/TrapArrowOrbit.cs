using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapArrowOrbit : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back * (-rotationSpeed * Time.deltaTime));
    }
}
