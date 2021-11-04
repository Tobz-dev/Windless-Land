using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    public float fallDelay = 2.0f;
    private FMOD.Studio.EventInstance PlatformFalling;

    void OnCollisionEnter(Collision collidedWithThis)
    {
        if (collidedWithThis.gameObject.tag == "Player")
        {
            StartCoroutine(FallAfterDelay());
            PlatformFalling = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/FallingPlatform");
            PlatformFalling.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            PlatformFalling.start();
            PlatformFalling.release();

        }
    }

    IEnumerator FallAfterDelay()
    {
        yield return new WaitForSeconds(fallDelay);
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
