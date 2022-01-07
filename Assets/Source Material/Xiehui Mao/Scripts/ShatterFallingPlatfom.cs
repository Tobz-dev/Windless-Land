using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Secondary Author: William Smith
public class ShatterFallingPlatfom : MonoBehaviour
{
    private Rigidbody rbd;
    private MeshCollider meshcollider;

    private FMOD.Studio.EventInstance PlatformFalling;

    public float fallplat;
    public float fallplaton;
    public bool isFalling = false;
    public Vector3 initialposition;


    void OnCollisionEnter(Collision collidedWithThis)
    {
        if (collidedWithThis.gameObject.tag == "Player")
        {
            Invoke("Fall", fallplat);
            PlatformFalling = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/FallingPlatform");
            PlatformFalling.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            PlatformFalling.start();
            PlatformFalling.release();

        }
    }

    private void Awake()
    {
        rbd = GetComponent<Rigidbody>();
        meshcollider = GetComponent<MeshCollider>();

    }

    private void Start()
    {
        initialposition = transform.position;
    }

    void Fall()
    {
        rbd.isKinematic = false;
        meshcollider.convex = true;
        meshcollider.isTrigger = true;

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
        meshcollider.convex = false;
        meshcollider.isTrigger = false;

        transform.position = initialposition;
        rbd.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Reset")

        {
            rbd.isKinematic = true;
            meshcollider.convex = false;
            meshcollider.isTrigger = false;

            isFalling = false;
            respawn();
        }
    }
}
