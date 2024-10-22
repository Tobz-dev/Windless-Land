using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Main Authour : Tim Ag�lii
public class CharacterControllerRemapTest : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerInputs inputActions;
    [SerializeField] private InputManager inputManager;

    [SerializeField]
    float moveSpeed = 4f;
    float dodgerollSpeed = 18f;
    float dodgerollDuration = 0.35f;
    float dodgerollCooldown = 0.2f;

    float moveSpeedDefault;



    float dodgeTimer = 0;

    float flaskTimer = 0;


    Quaternion lookRotation;


    private Plane plane;

    bool moveAllow = true;

    bool invincibility = false;



    //healthFlask
    bool healthFlaskTimerRunning = true;
    bool healthFlaskStart = false;
    bool healthFlaskOfCooldown = true;

    float flaskUses = 4;
    float healthFlaskSpeedFactor = 0.2f;
    float healthFlaskDuration = 1.5f;
    float healthFlaskCooldown = 0.5f;

    //attack
    bool startAttackCooldown = false;
    float attackTimer = 0;

    //dodgeroll
    bool dodgerollTimerRunning = false;
    bool dodgerollStart = false;
    bool dodgerollRemapTrigger = false;
    bool dodgerolling = false;
    bool dodgerollOfCooldown = true;
    Vector3 inputDirection;

    private bool canMove = true;


    //hitbox variables

    [SerializeField]
    private GameObject attackHitbox;

    [SerializeField]
    private float swingTime;
    [SerializeField]
    private Vector3 hitboxOffset;


    [SerializeField]
    private float xRotationOffset;
    [SerializeField]
    private float yRotationOffset;
    [SerializeField]
    private float zRotationOffset;


    [SerializeField]
    private Animator anim;

    public Transform respawnPoint;

    Vector3 forward, right;

    //Move
    Vector2 movementInput;

    private void OnEnable()
    {
        inputActions.WindlessLand.Dodgeroll.started += Dodgeroll;
        inputActions.WindlessLand.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.WindlessLand.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Start is called before the first frame update
    void Awake()
    {
        canMove = true;
        plane = new Plane(Vector3.up, Vector3.zero);
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        moveSpeedDefault = moveSpeed;

        playerInput = GetComponent<PlayerInput>();
        inputActions = new PlayerInputs();
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

            PlayerRotationUpdate();

            HealthFlaskManager();

            DodgerollManager();

            AttackManager();

            if (Input.anyKey && moveAllow == true)
            {
                Move();
            }

            //anim stuff here. 
            anim.SetFloat("XSpeed", movementInput.x);//Input.GetAxis("HorizontalKey"));
            anim.SetFloat("YSpeed", movementInput.y);//Input.GetAxis("VerticalKey"));
        }
    }

    private void PlayerRotationUpdate()
    {

        if (moveAllow && (Mathf.Abs(movementInput.x) + Mathf.Abs(movementInput.y)) !=0) //Input.GetAxis("HorizontalKey")) + Mathf.Abs(Input.GetAxis("VerticalKey"))) != 0)
        {
            Vector3 horizontal = movementInput.x * right; //(Input.GetAxis("Horizontal") * right);
            Vector3 vertical = movementInput.y * forward; // (Input.GetAxis("Vertical") * forward);
            Vector3 rotation = horizontal + vertical;

            transform.rotation = Quaternion.LookRotation(rotation);
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
        /*
        int horizontalMovement = 0;
        int verticalMovement = 0;
        if (playerInput.actions["Move Up"].triggered)
        {
            verticalMovement = 1;
        }
        else if (playerInput.actions["Move Down"].triggered)
        {
            verticalMovement = -1;
        }
        if (playerInput.actions["Move Right"].triggered)
        {
            horizontalMovement = 1;
        }
        else if (playerInput.actions["Move Left"].triggered)
        {
            horizontalMovement = -1;
        }
        */

        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");
        Vector3 horizontalMovement = right * moveSpeed * Time.deltaTime * movementInput.x;
        Vector3 verticalMovement = forward * moveSpeed * Time.deltaTime * movementInput.y;
        //Debug.Log("speed x:" + movementInput.x + "speed y:" + movementInput.y);
        //Vector3 playerMovement = rightMovement + upMovement;
        Vector3 playerMovement = horizontalMovement + verticalMovement;

        inputDirection = playerMovement.normalized;

        if (playerMovement.magnitude > moveSpeed * Time.deltaTime)
        {
            playerMovement = playerMovement.normalized * moveSpeed * Time.deltaTime;
        }

        transform.position += playerMovement;

    }

    void HealthFlaskManager()
    {
        if (inputActions.WindlessLand.HealthRefill.triggered/*playerInput.actions["Health Refill"].triggered,,,, Input.GetKeyDown(KeyCode.Q)*/ && healthFlaskOfCooldown && flaskUses > 0)
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

                    healthFlaskTimerRunning = false;
                    moveSpeed = moveSpeedDefault;
                }
                else
                {




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

            if (dodgerollStart == true || startAttackCooldown == true)
            {

                healthFlaskStart = false;
                healthFlaskOfCooldown = true;
                healthFlaskTimerRunning = true;
                moveSpeed = moveSpeedDefault;
                flaskTimer = 0;

            }
        }
    }


    void Dodgeroll(InputAction.CallbackContext obj)
    {
        dodgerollRemapTrigger = true;
        Debug.Log("dodgerollworks");
    }

    void DodgerollManager()
    {
        if (/*inputActions.WindlessLand.Dodgeroll.triggered ,,
playerInput.actions["Dodgeroll"].triggered &&*/ dodgerollRemapTrigger && dodgerollOfCooldown)
        //Input.GetKeyDown(KeyCode.Space) && dodgerollOfCooldown)
        {
            dodgerollStart = true;
            dodgerollTimerRunning = true;
            dodgerollRemapTrigger = false;

            //more anim things
            Debug.Log("in player Dodgeroll");
            anim.SetTrigger("DodgeRoll");
        }

        if (dodgerollStart == true)
        {

            if (dodgerollTimerRunning == true)
            {
                if (DodgeWaitTimer(dodgerollDuration))
                {

                    moveAllow = true;
                    dodgerolling = false;
                    dodgerollTimerRunning = false;
                    invincibility = false;
                }
                else
                {
                    transform.rotation = Quaternion.LookRotation(inputDirection);
                    transform.position += (transform.forward).normalized * dodgerollSpeed * Time.deltaTime;
                    moveAllow = false;
                    dodgerolling = true;
                    dodgerollOfCooldown = false;
                    invincibility = true;


                }
            }
            else
            {
                if (DodgeWaitTimer(dodgerollCooldown))
                {
                    dodgerollStart = false;
                    dodgerollRemapTrigger = false;
                    dodgerollOfCooldown = true;


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

    void AttackManager()
    {
        if (inputActions.WindlessLand.Attack.triggered/*playerInput.actions["Attack"].triggered,, /*Input.GetKeyDown(KeyCode.Mouse0)*/ && startAttackCooldown == false && dodgerollTimerRunning == false && healthFlaskStart == false)
        {
            Attack();

        }

        AttackCoolDown();
    }

    void Attack()
    {
        transform.rotation = lookRotation;
        InstantiateAttackHitbox();
        startAttackCooldown = true;

        //more anim things
        //Debug.Log("in player attack");
        anim.SetTrigger("Attack");
    }
    void AttackCoolDown()
    {
        if (startAttackCooldown == true)
        {
            if (AttackWaitTimer(swingTime))
            {
                moveAllow = true;

                startAttackCooldown = false;
            }
            else
            {
                moveAllow = false;
            }
        }

    }

    void InstantiateAttackHitbox()
    {

        Instantiate(attackHitbox, transform.position + (transform.rotation * new Vector3(0, 0, 2f)), transform.rotation);
        //GameObject hitBox = (GameObject)Instantiate(attackHitbox, transform.position + (transform.rotation * hitboxOffset), transform.rotation * Quaternion.Euler(xRotationOffset, yRotationOffset, zRotationOffset));

        //hitBox.transform.localScale = hitboxScale;


        //hitBox.GetComponent<newHitbox>().SetTarget("Enemy");
        //hitBox.GetComponent<newHitbox>().SetDamage(damage);
        //hitBox.GetComponent<newHitbox>().SetSwingTime(swingTime);



    }

    private bool AttackWaitTimer(float seconds)
    {

        attackTimer += Time.deltaTime;

        if (attackTimer >= seconds)
        {

            attackTimer = 0;
            return true;


        }
        return false;
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

    public bool GetInvincibility()
    {
        return invincibility;
    }


    public void Respawn()
    {
        Debug.Log("Player Dead");
        GetComponent<HealthScript>().regainHealth(100);
        transform.position = respawnPoint.transform.position;
    }

    public float GetFlaskUses()
    {

        return flaskUses;
    }

}


