using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWeaponSwing : MonoBehaviour
{
    public GameObject hitboxPrefab;
    public Transform currentLocation;
    public Vector3 location;

    public int numToShow = 4;

    public float rotation = 90;
    public float rotationOff = 45;

    public Vector3 Offset;

    public float xOffset = 0f;

    public float xRotationOffset = 0f;
    public float yRotationOffset = 0f;
    public float zRotationOffset = 0f;

    private HitboxTestScript characterController;


    [SerializeField]
    private float SwingDelay = 0.5f;
    [SerializeField]
    private float SwingTime = 0.4f;
    [SerializeField]
    private float SwingCoolDown = 2.4f;
    private bool CanSwing = true;
    [SerializeField]
    private int damage = 25;



    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("TEST");

            location = transform.position;

            //location = location + Offset;
            location = location + (transform.forward * xOffset);

            //location = Quaternion.AngleAxis(-45, Vector3.up) * location;

            //rotation = transform.localRotation.eulerAngles.x;



            //GameObject hitboxClone = (GameObject)Instantiate(hitboxPrefab, location, Quaternion.identity);
            //GameObject.Instantiate(hitboxPrefab, transform.position, transform.rotation * Quaternion.Euler(0f, rotation, 0f));

            GameObject hitboxClone = (GameObject)Instantiate(hitboxPrefab, location, transform.rotation * Quaternion.Euler(0f, rotation + 45f, 0f));
            

            //characterController = hitboxClone.GetComponent<HitboxTestScript>();

            hitboxClone.GetComponent<HitboxTestScript>().shwString();



            hitboxClone.GetComponent<HitboxTestScript>().numTest = numToShow;

            //hitboxClone.GetComponent<HitboxTestScript>().shwString();


            //hitboxClone.GetComponent<HitboxTestScript>();


            Destroy(hitboxClone, SwingTime);

        }

    }
}
