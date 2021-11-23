using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapArrowShaftBillboard : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private float zRotation;

    private void Awake()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion toRotation = Quaternion.FromToRotation(transform.up, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.1f * Time.time);

        //Vector3 difference = cameraTRANS.position - transform.position;
        //float rotationZ = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
        //transform.localRotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);


        //zRotation = 45 - (parent.transform.localEulerAngles.y / 2);
        //transform.localRotation = Quaternion.Euler(parent.transform.rotation.x,
        //                                           parent.transform.rotation.y,
        //                                           zRotation);
    }

}
