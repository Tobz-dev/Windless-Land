using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class test : MonoBehaviour
{
    public BoxCollider col;
    public MeshRenderer mesh;

    [SerializeField]
    private float SwingDelay = 0.5f;
    [SerializeField]
    private float SwingTime = 0.4f;
    [SerializeField]
    private float SwingCoolDown = 2.4f;
    private bool CanSwing = true;
    [SerializeField]
    private int damage = 25;


private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit");
            other.GetComponent<HealthScript>().takeDamage(damage);
        }
    }
    private void Awake()
    {
        col = GetComponent<BoxCollider>();
        mesh = GetComponent<MeshRenderer>();
        //col.enabled = !col.enabled;
        //mesh.enabled = !mesh.enabled;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && CanSwing)
        {
            Debug.Log("Key Down");
            CanSwing = false;
            //disable movement
            StartCoroutine(Timer(Swing, SwingDelay));
        }
    }


    private IEnumerator Timer(Action method, float x)
    {
        yield return new WaitForSeconds(x);
        //Debug.Log("Run Method");
        method();

    }

    private void Swing()
    {
        Debug.Log("Swing");
        col.enabled = !col.enabled;
        mesh.enabled = !mesh.enabled;
        StartCoroutine(Timer(SwingDone, SwingTime));
    }

    private void SwingDone()
    {
        Debug.Log("SwingDone");
        col.enabled = !col.enabled;
        mesh.enabled = !mesh.enabled;
        //enable movement
        StartCoroutine(Timer(SwingCooldown, SwingCoolDown));
    }

    private void SwingCooldown()
    {
        Debug.Log("Cool down done");
        CanSwing = true;
    }
}
