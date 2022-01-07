using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Main Author: Tobias Martinsson
//secondary Author: William Smith
public class ArrowScript : MonoBehaviour
{
    [SerializeField]
    private GameObject bigArrowPrefab;
    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private Transform arrowSpawnPosition;
    [SerializeField]
    private float shootForce = 10f;
    [SerializeField]
    private float arrowLifeTime;
    [SerializeField]

    private FMOD.Studio.EventInstance ArrowShot;

    //Instantiates an arrow and shoots it in the 'forward' direction of parent object with desired shootforce.
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


    //Shoots the bosses big arrow, to destroy pillars in the boss level.
    public void shootBigArrow()
    {
        if (bigArrowPrefab != null)
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
