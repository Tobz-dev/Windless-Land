using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 4f;
    private Plane plane;
    private bool canMove = true;

    [SerializeField]
    private Animator anim;

    Vector3 forward, right;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        plane = new Plane(Vector3.up, Vector3.zero);
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out var enter) && canMove == true)
        {
            var hitPoint = ray.GetPoint(enter);
            plane.SetNormalAndPosition(Vector3.up, transform.position);
            var playerPositionOnPlane = plane.ClosestPointOnPlane(transform.position);
            transform.rotation = Quaternion.LookRotation(hitPoint - playerPositionOnPlane);
        }
        if (Input.anyKey && canMove == true)
        {
            Move();
        }



        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("Key Down");
            anim.SetTrigger("Attack");
            CanMove();

        }


        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log(anim.GetFloat("XSpeed"));
    }

    void Move()
    {
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");
        transform.position += rightMovement;
        transform.position += upMovement;

        //some stuff to help test the animations
        anim.SetFloat("XSpeed", Input.GetAxis("HorizontalKey"));
        anim.SetFloat("YSpeed", Input.GetAxis("VerticalKey"));
    }

    public void CanMove()
    {
        if (canMove == true)
        {
            canMove = false;
            Debug.Log("canMove = false");
        }
        else
        {
            canMove = true;
            Debug.Log("canMove = true");
        }
    }
}
