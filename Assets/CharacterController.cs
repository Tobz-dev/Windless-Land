using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 4f;
    float DodgerollSpeed = 18f;
    float DodgerollDuration = 0.35f;
    float DodgerollCooldown = 0.5f;

    float moveSpeedDefault;

    float flaskUses = 4;
    float healthFlaskSpeedFactor = 0.2f;
    float healthFlaskDuration = 1.5f;
    float healthFlaskCooldown = 0.5f;

    float dodgeTimer = 0;

    float flaskTimer = 0;


    Quaternion lookRotation;

    Quaternion moveRotation;

    private Plane plane;

    bool MoveAllow = true;

    bool invisibility = false;

    //healthFlask
    bool healthFlaskTimerRunning = true;
    bool healthFlaskStart = false;
    bool healthFlasking = false;
    bool healthFlaskOfCooldown = true;


    //dodgeroll
    bool DodgerollTimerRunning = true;
    bool DodgerollStart = false;
    bool Dodgerolling = false;
    bool DodgerollOfCooldown = true;

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

        moveSpeedDefault = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float enter) && canMove == true)
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            plane.SetNormalAndPosition(Vector3.up, transform.position);
            Vector3 playerPositionOnPlane = plane.ClosestPointOnPlane(transform.position);

            lookRotation = Quaternion.LookRotation(hitPoint - playerPositionOnPlane);

            playerRotationUpdate();

            healthFlaskManager();

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
            Vector3 horizontal = (Input.GetAxis("Horizontal") * right);
            Vector3 vertical = (Input.GetAxis("Vertical") * forward);
            Vector3 rotation = horizontal + vertical;
                
            transform.rotation = Quaternion.LookRotation(rotation);
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

        Vector3 playerMovement = rightMovement + upMovement;


        if (playerMovement.magnitude > moveSpeed * Time.deltaTime)
        {
            playerMovement = playerMovement.normalized * moveSpeed * Time.deltaTime;
        }

        transform.position += playerMovement;

    }

    void healthFlaskManager()
    {
        if (Input.GetKeyDown(KeyCode.Q) && healthFlaskOfCooldown && flaskUses > 0)
        {
            healthFlaskStart = true;

        }



        if (healthFlaskStart == true)
        {




            if (healthFlaskTimerRunning == true)
            {
                if (FlaskWaitTimer(healthFlaskDuration))
                {

                    GetComponentInParent<HealthScript>().regainHealth(1);
                    healthFlasking = false;
                    healthFlaskTimerRunning = false;
                    moveSpeed = moveSpeedDefault;
                }
                else
                {


                    healthFlasking = true;

                    moveSpeed = moveSpeedDefault * healthFlaskSpeedFactor;

                    healthFlaskOfCooldown = false;

                }
            }
            else
            {
                if (FlaskWaitTimer(healthFlaskCooldown))
                {
                    flaskUses--;
                    healthFlaskStart = false;
                    healthFlaskOfCooldown = true;
                    healthFlaskTimerRunning = true;

                }
            }

            if (DodgerollStart == true)
            {
                
                healthFlasking = false;
                healthFlaskStart = false;
                healthFlaskOfCooldown = true;
                healthFlaskTimerRunning = true;
                moveSpeed = moveSpeedDefault;
                flaskTimer = 0;


            }

        }
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
                if (DodgeWaitTimer(DodgerollDuration))
                {

                    MoveAllow = true;
                    Dodgerolling = false;
                    DodgerollTimerRunning = false;
                    invisibility = false;
                }
                else
                {
                    transform.position += (transform.forward).normalized * DodgerollSpeed * Time.deltaTime;
                    MoveAllow = false;
                    Dodgerolling = true;
                    DodgerollOfCooldown = false;
                    invisibility = true;

                }
            }
            else
            {
                if (DodgeWaitTimer(DodgerollCooldown))
                {

                    DodgerollStart = false;
                    DodgerollOfCooldown = true;
                    DodgerollTimerRunning = true;

                }
            }
        }
    }

    private bool DodgeWaitTimer(float seconds)
    {

        dodgeTimer += Time.deltaTime;

        if (dodgeTimer >= seconds)
        {

            dodgeTimer = 0;
            return true;


        }
        return false;
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

    private bool FlaskWaitTimer(float seconds)
    {

        flaskTimer += Time.deltaTime;

        if (flaskTimer >= seconds)
        {

            flaskTimer = 0;
            return true;

        }

        return false;
    }

    public bool GetInvisibility() {
        return invisibility;
    }

}


