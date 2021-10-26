using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    private Rigidbody rbd;
    private BoxCollider boxcollider;

    public float fallplat;

    public float fallplaton;

    public bool isFalling = false;

    public Vector3 initialposition;

    private void Awake()
    {
        rbd = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        initialposition = transform.position;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Invoke("Fall", fallplat);
        }
    }

    void Fall()
    {
        rbd.isKinematic = false;
        boxcollider.isTrigger = true;
        isFalling = true;
    }

    void respawn()
    {
        StartCoroutine(respawnco());
    }

    IEnumerator respawnco()
    {
        yield return new WaitForSeconds(fallplaton);
        isFalling = false;
        rbd.isKinematic = true;
        boxcollider.isTrigger = false;
        transform.position = initialposition;
        rbd.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Death")

        {
            rbd.isKinematic = true;
            boxcollider.isTrigger = false;
            isFalling = false;
            respawn();
        }
    }
}
