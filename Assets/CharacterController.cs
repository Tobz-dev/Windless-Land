using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 4f;
    private Plane plane;
    private bool canMove = true;

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

        /*
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("Key Down");
            CanMove();

        }
        */
    }

    void Move()
    {
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");
        transform.position += rightMovement;
        transform.position += upMovement;
    }

    public void CanMove()
    {
        if(canMove == true)
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
