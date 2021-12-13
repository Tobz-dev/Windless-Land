using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerArrowVFX : MonoBehaviour
{

    [SerializeField]
    private VisualEffect impactEffect;
    [SerializeField]
    private VisualEffect pulseEffect;
    [SerializeField]
    private VisualEffect fireEffect;
    [SerializeField]
    private GameObject[] childObjects;


    private float killTime = 0f;
    [SerializeField]
    private float speed = 30f;

    // Start is called before the first frame update
    void Awake()
    {
        pulseEffect.SendEvent("PlayPulse");
        fireEffect.SendEvent("PlayBowFire");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        killTime += 1 * Time.deltaTime;
        if (killTime >= 5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            impactEffect.SendEvent("PlayImpact");
            for (int i = 0; i < childObjects.Length; i++)
            {
                speed = 0f;
                childObjects[i].SetActive(false);
            }
        }
    }
}
