using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 4f;
    float DodgerollSpeed = 18f;
    float DodgerollTimer = 0;
    float DodgerollDuration = 0.35f;
    float DodgerollCooldown = 0.5f;

    Quaternion lookRotation;

    Quaternion moveRotation;

    private Plane plane;

    bool MoveAllow = true;

    bool DodgerollTimerRunning = true;
    bool DodgerollStart = false;
    bool Dodgerolling = false;
    bool DodgerollOfCooldown = true;

    Vector3 forward, right;
    // Start is called before the first frame update
    void Start()
    {
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
        if (plane.Raycast(ray, out var enter))
        {
            plane.SetNormalAndPosition(Vector3.up, transform.position);
            var hitPoint = ray.GetPoint(enter);
            var playerPositionOnPlane = plane.ClosestPointOnPlane(transform.position);

            lookRotation = Quaternion.LookRotation(hitPoint - playerPositionOnPlane);

            playerRotationUpdate();

           
            DodgerollManager();

            if (Input.anyKey && MoveAllow == true)
            {
                Move();
            }
        }
    }

    private void playerRotationUpdate()
    {
       
        if (MoveAllow && (Mathf.Abs(Input.GetAxis("HorizontalKey")) + Mathf.Abs(Input.GetAxis("VerticalKey"))) != 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

        }

    }
    //test
    void Move()
        {
            Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
            Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");

            Vector3 playerMovement = rightMovement + upMovement;
       

        if (playerMovement.magnitude > moveSpeed * Time.deltaTime)
            {
                playerMovement = playerMovement.normalized * moveSpeed * Time.deltaTime;
            }

            transform.position += playerMovement;

        }




        void DodgerollManager()
        {
        if (Input.GetKeyDown(KeyCode.Space) && DodgerollOfCooldown)
        {
            DodgerollStart = true;
        }

        if (DodgerollStart == true)
            {

                if (DodgerollTimerRunning == true)
                {
                    if (DodgerollWaitTime(DodgerollDuration))
                    {

                        MoveAllow = true;
                        Dodgerolling = false;
                        DodgerollTimerRunning = false;
                    }
                    else
                    {
                        transform.position += (transform.forward + transform.right).normalized * DodgerollSpeed * Time.deltaTime;
                        MoveAllow = false;
                        Dodgerolling = true;
                        DodgerollOfCooldown = false;

                    }
                }
                else
                {
                    if (DodgerollWaitTime(DodgerollCooldown))
                    {

                        DodgerollStart = false;
                        DodgerollOfCooldown = true;
                        DodgerollTimerRunning = true;

                    }
                }
            }
        }

        private bool DodgerollWaitTime(float seconds)
        {

            DodgerollTimer += Time.deltaTime;

            if (DodgerollTimer >= seconds)
            {

                DodgerollTimer = 0;
                return true;

            }

            return false;
        }

    }
