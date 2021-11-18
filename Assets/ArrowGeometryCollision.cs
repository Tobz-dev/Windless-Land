using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGeometryCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 11)
        {
            Destroy(gameObject);
        }
    }
}
