using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public GameObject bigArrowPrefab;
    public GameObject arrowPrefab;
    public Transform arrowSpawnPosition;
    public float shootForce = 10f;
    public float arrowLifeTime;

    private FMOD.Studio.EventInstance ArrowShot;

    public void shootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPosition.position, arrowSpawnPosition.rotation);

        arrow.GetComponent<Rigidbody>().velocity = arrowSpawnPosition.transform.forward * shootForce;

        ArrowShot = FMODUnity.RuntimeManager.CreateInstance("event:/Objects/ArrowShot");
        ArrowShot.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        ArrowShot.start();
        ArrowShot.release();

        Destroy(arrow, arrowLifeTime);
    }

    public void shootBigArrow()
    {
        if(bigArrowPrefab != null)
        {
            Vector3 bigSpawnPosition = new Vector3(arrowSpawnPosition.position.x, arrowSpawnPosition.position.y + 1f, arrowSpawnPosition.position.z);
            GameObject arrow = Instantiate(bigArrowPrefab, bigSpawnPosition, arrowSpawnPosition.rotation);

            arrow.GetComponent<Rigidbody>().velocity = arrowSpawnPosition.transform.forward * shootForce * 0.25f;

            ArrowShot = FMODUnity.RuntimeManager.CreateInstance("event:/Objects/ArrowShot");
            ArrowShot.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            ArrowShot.start();
            ArrowShot.release();

            Destroy(arrow, arrowLifeTime);
        }
        
    }
}
