using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBarrels : MonoBehaviour
{
    [SerializeField] private GameObject destroyedBarrel;
    public float bforce = 1f;
    protected Rigidbody rb;
    private int active = 0;

    
    [SerializeField] private GameObject sword;







    public void destroyBarrel()
    {
        
        Instantiate(destroyedBarrel, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
