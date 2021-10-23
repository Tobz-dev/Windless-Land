using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHelper : MonoBehaviour
{
    private float time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime * 0.65f;


        time = Mathf.Clamp(time, 0.0f, 1.1f);

        //Vector3 vector3 = new Vector3(0.3f, 0.3f, 0.3f);
        transform.localScale = Vector3.one * Mathfx.Berp(0f, 1f, time);
    }

    public void OnDisable()
    {
        transform.localScale = new Vector3(0f, 0f, 0f);
        time = 0f;
    }

    public void OnEnable()
    {   
        transform.localScale = new Vector3(0f, 0f, 0f);
        Debug.Log("in enable. localscale is" + transform.localScale);
        time = 0f;
    }
}
