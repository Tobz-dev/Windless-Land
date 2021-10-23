using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingArrowTrap : MonoBehaviour
{
    private float timeLeft = 0.1f;
    private float originalTime;
    // Start is called before the first frame update
    void Start()
    {
        originalTime = timeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            GetComponent<ArrowScript>().shootArrow();
            timeLeft = originalTime;
        }
    }
}
