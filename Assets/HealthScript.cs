using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private Material material;
    [SerializeField]
    private Material originalMaterial;

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            //death animation and delay
            Destroy(gameObject);
        }
    }

    //damage handler
    public void takeDamage(int x)
    {
        health -= x;
        StartCoroutine(damagedMaterial()); 
        damagedMaterial();
        
    }

    private IEnumerator damagedMaterial()
    {
        gameObject.GetComponent<MeshRenderer>().material = material;
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<MeshRenderer>().material = originalMaterial;
    }
}
