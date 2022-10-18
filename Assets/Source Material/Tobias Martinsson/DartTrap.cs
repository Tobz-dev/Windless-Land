using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tobias Martinsson
public class DartTrap : MonoBehaviour
{
    [SerializeField]
    private float shootCooldown = 1;
    private float originalTime;

    [SerializeField]
    private float firstShotDelay;

    // Start is called before the first frame update
    void Start()
    {
        originalTime = shootCooldown;
        shootCooldown += firstShotDelay;
    }

    // Update is called once per frame
    void Update()
    {
        shootCooldown -= Time.deltaTime;
        if (shootCooldown < 0)
        {
            GetComponent<ArrowScript>().shootArrow();
            shootCooldown = originalTime;
        }
    }
}
