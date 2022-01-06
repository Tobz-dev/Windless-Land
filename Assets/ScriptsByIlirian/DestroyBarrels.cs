using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBarrels : MonoBehaviour
{
    [SerializeField] private GameObject destroyedBarrel;


    
    //[SerializeField] private GameObject sword;







    public void destroyBarrel()
    {
        
        Instantiate(destroyedBarrel, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
