using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    private Rigidbody rbd;
    private BoxCollider boxcollider;
    private MeshRenderer meshRenderer;
    
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
        boxcollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        initialposition = transform.position;
    }

    void Fall()
    {
        rbd.isKinematic = false;
        boxcollider.isTrigger = true;
        
        isFalling = true;
    }

    public void respawn()
    {
        StartCoroutine(respawnco());
    }

    IEnumerator respawnco()
    {
        yield return new WaitForSeconds(fallplaton);
        isFalling = false;
        rbd.isKinematic = true;
        boxcollider.isTrigger = false;
        meshRenderer.enabled = true;

        transform.position = initialposition;
        rbd.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Reset")

        {
            meshRenderer.enabled = false;
            rbd.isKinematic = true;
            boxcollider.isTrigger = false;
           
            isFalling = false;
            respawn();
        }
    }
}

