using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTrap : MonoBehaviour
{

    private float timeLeft = 1;
    private float originalTime;
    // Start is called before the first frame update
    void Start()
    {
        originalTime = timeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            GetComponent<ArrowScript>().shootArrow();
            timeLeft = originalTime;
        }
    }
}
