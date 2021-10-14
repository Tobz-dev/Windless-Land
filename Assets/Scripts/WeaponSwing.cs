using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwing : MonoBehaviour
{

    public BoxCollider col;
    public MeshRenderer mesh;

    public float SwingTime = 0.4f;
    public float SwingCoolDown = 2.4f;
    private bool CanSwing = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit");
        }
    }

    private void Awake()
    {
        col = GetComponent<BoxCollider>();
        mesh = GetComponent<MeshRenderer>();
        col.enabled = !col.enabled;
        mesh.enabled = !mesh.enabled;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && CanSwing)
        {
            Debug.Log("Key Down");
            CanSwing = false;
            StartCoroutine(SwingTimer(SwingTime));
            //StartCoroutine(SwingCooldown(SwingCoolDown));
        }
    }




    private IEnumerator SwingCooldown(float x)
    {
        yield return new WaitForSeconds(x);
        Debug.Log("Cool down done");
        CanSwing = true;

    }

    private IEnumerator SwingTimer(float x)
    {
        col.enabled = !col.enabled;
        mesh.enabled = !mesh.enabled;
        yield return new WaitForSeconds(x);
        Debug.Log("Swing Done");
        StartCoroutine(SwingCooldown(SwingCoolDown));
        col.enabled = !col.enabled;
        mesh.enabled = !mesh.enabled;

    }








    /*public BoxCollider col;

    public int damage = 1;

    /*private void Awake()
    {
        col = GetComponent<BoxCollider>();
        //gameObject.SetActive(false);
    }
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Key Down");
        }
    }

    private void OnTriggerEnter(Collision collision)
    {
    
        if (collision.gameObject.tag == "Enemy")
        {

            Debug.Log("Hit");
            
            //Enemy Damage

        }
    }
    */
}
