using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Ilirian Zuta
public class DestroyBarrels : MonoBehaviour
{
    [SerializeField] private GameObject destroyedBarrel;


    







    public void destroyBarrel()
    {
        
        Instantiate(destroyedBarrel, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
