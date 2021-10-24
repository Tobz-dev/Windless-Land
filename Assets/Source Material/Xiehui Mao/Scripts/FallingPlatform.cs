using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    public float fallDelay = 2.0f;

    void OnCollisionEnter(Collision collidedWithThis)
    {
        if (collidedWithThis.gameObject.name == "Cube")
        {
            StartCoroutine(FallAfterDelay());
        }
    }

    IEnumerator FallAfterDelay()
    {
        yield return new WaitForSeconds(fallDelay);
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
