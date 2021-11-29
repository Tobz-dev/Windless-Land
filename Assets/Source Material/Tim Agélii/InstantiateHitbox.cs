using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateHitbox : MonoBehaviour
{
    [SerializeField]
    private GameObject hitBox;
    // Start is called before the first frame update

   public void InstantiateAttackHitbox()
    {
        GameObject newHitbox = Instantiate(hitBox, transform.position + (transform.rotation * new Vector3(0, 0.5f, 1.7f)), transform.rotation);

        newHitbox.transform.parent = gameObject.transform;


    }
}
