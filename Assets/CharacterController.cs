using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main Authour : Tim Agélii
public class CharacterController : MonoBehaviour
{

    [SerializeField]
    float moveSpeed;

  
    float moveSpeedDefault;

   

    float dodgeTimer = 0;

    float flaskTimer = 0;

    Vector3 rotationOffset = new Vector3(90, 0, 0);

    Quaternion lookRotation;


    private Plane plane;

    bool moveAllow = true;

    bool rotationAllow = true;

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
    [SerializeField]
    float dodgerollDuration = 0.7f;
    [SerializeField]
    float dodgerollCooldown = 0.2f;


    [SerializeField]
    float dodgerollSpeed = 11f;
    [SerializeField]
    float dodgerollDropSpeed = 3f;
    bool dodgerollTimerRunning = false;
    bool dodgerollStart = false;
    bool dodgerolling = false;
    bool dodgerollOfCooldown = true;
    Vector3 inputDirection;

    Vector3 playerMovement;

    private bool canMove = true;

    [SerializeField]
    private float dodgeDropOffTime;
    //hitbox variables

    [SerializeField]
    private GameObject attackHitbox;

    [SerializeField]
    private float timeToNextSwing;

    private bool startAttackDelay = false;
    
    [SerializeField]
    private float attackDelay;

    [SerializeField]
    private float swingCooldown = 0.6f;

    //prototyp
    float extraInputTimeDelay = 0.05f;
    bool queueAttack = false;
    bool queueDodge = false;

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

    private FMOD.Studio.EventInstance HealthRefill;
    private FMOD.Studio.EventInstance Dead;


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

            PlayerRotationUpdate();

            HealthFlaskManager();

            DodgerollManager();

            AttackManager();

            UpdateMoveInput();

            if (Input.anyKey && moveAllow == true)
            {
                Move();
            }

            //anim stuff here. 
            anim.SetFloat("XSpeed", Input.GetAxis("HorizontalKey"));
            anim.SetFloat("YSpeed", Input.GetAxis("VerticalKey"));
        }
    }

    private void PlayerRotationUpdate()
    {

        if (moveAllow && rotationAllow && (Mathf.Abs(Input.GetAxis("HorizontalKey")) + Mathf.Abs(Input.GetAxis("VerticalKey"))) != 0)
        {
            Vector3 horizontal = (Input.GetAxis("Horizontal") * right);
            Vector3 vertical = (Input.GetAxis("Vertical") * forward);
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

    void UpdateMoveInput() {
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");

       playerMovement = rightMovement + upMovement;

        inputDirection = playerMovement.normalized;

        if (playerMovement.magnitude > moveSpeed * Time.deltaTime)
        {
            playerMovement = playerMovement.normalized * moveSpeed * Time.deltaTime;
        }
    }

    void Move()
    {
     

        transform.position += playerMovement;

    }

    void HealthFlaskManager()
    {
        if (Input.GetKeyDown(KeyCode.Q) && healthFlaskOfCooldown && flaskUses > 0 && startAttackDelay == false && startAttackCooldown == false)
        {
            healthFlaskStart = true;
            HealthRefill = FMODUnity.RuntimeManager.CreateInstance("event:/Game/HealthRefill");
            HealthRefill.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            HealthRefill.start();
            HealthRefill.release();

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

            if (dodgerollStart == true)
            {
                
                healthFlaskStart = false;
                healthFlaskOfCooldown = true;
                healthFlaskTimerRunning = true;
                moveSpeed = moveSpeedDefault;
                flaskTimer = 0;

            }
        }
    }

    void StartDodgeroll() {
        
            dodgerollStart = true;
            dodgerollTimerRunning = true;

            //more anim things
            //Debug.Log("in player Dodgeroll");
            anim.SetTrigger("DodgeRoll");



        transform.rotation = Quaternion.LookRotation(inputDirection);
        moveAllow = false;

        dodgerolling = true;
        dodgerollOfCooldown = false;
        invincibility = true;
    }

    void DodgerollManager()
    {
    if (Input.GetKeyDown(KeyCode.Space) && dodgerollOfCooldown && startAttackDelay == false && startAttackCooldown == false)
    {
        StartDodgeroll();
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
                   
                }
                else
                {
                    if (dodgeTimer < dodgeDropOffTime)
                    {
                        transform.position += (transform.forward).normalized * dodgerollSpeed * Time.deltaTime;
                    }
                    else {
                        invincibility = false;
                        transform.position += (transform.forward).normalized * dodgerollDropSpeed * Time.deltaTime;
                    }
                   
                 
                }
            }
            else
            {
                if (DodgeWaitTimer(dodgerollCooldown))
                {
                    dodgerollStart = false;
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

    void AttackManager() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && startAttackDelay == false && startAttackCooldown == false && dodgerollTimerRunning == false && healthFlaskStart == false) 
        {


            Attack();
           
        }

        HitboxDelay();

        AttackCoolDown();
    }

    void Attack()
    {


       
        //more anim things
        //Debug.Log("in player attack");
        anim.SetTrigger("Attack");

        moveAllow = false;
        transform.rotation = lookRotation;
        startAttackDelay = true;

    }
    void AttackCoolDown()
    {
        if (startAttackCooldown == true)
        {
            if (AttackWaitTimer(swingCooldown))
            {
               moveAllow = true;

                startAttackCooldown = false;

                anim.SetTrigger("StopAttack");

            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Debug.Log("ATACK");
                    queueAttack = true;
                    queueDodge = false;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("DODGE");
                    queueAttack = false;
                    queueDodge = true;
                }

                if (attackTimer >= timeToNextSwing)
                {

                    if (queueAttack == true)
                    {

                        startAttackCooldown = false;
                        queueAttack = false;
                        attackTimer = 0;
                        anim.SetTrigger("StopAttack");
                        Attack();
                    }
                    if (queueDodge == true)
                    {
                        startAttackCooldown = false;
                        queueDodge = false;
                        attackTimer = 0;
                        anim.SetTrigger("StopAttack");
                        StartDodgeroll();
                    }
                }
            }
        }
    }
    void HitboxDelay()
    {
        if (startAttackDelay == true)
        {
            if (AttackWaitTimer(attackDelay))
            {

                InstantiateAttackHitbox();
                startAttackDelay = false;
                startAttackCooldown = true;


            }
            else
            {
                
                transform.position += (transform.forward).normalized * 2f * Time.deltaTime;
                if (attackTimer >= extraInputTimeDelay && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Debug.Log("ATACK");
                    queueAttack = true;
                    queueDodge = false;
                }
                if (attackTimer >= (extraInputTimeDelay) && Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("DODGE");
                    queueAttack = false;
                    queueDodge = true;
                }


            }
        }
     
    }

    void InstantiateAttackHitbox()
    {
        var newHitbox = Instantiate(attackHitbox, transform.position + (transform.rotation * new Vector3(0, 0, 1.2f)), transform.rotation);

        newHitbox.transform.parent = gameObject.transform;
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

    public bool GetInvincibility() {
        return invincibility;
    }


    public void Respawn()
    {
        Debug.Log("Player Dead");
        GetComponent<HealthScript>().regainHealth(100);
        Dead = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Player/Dead");
        Dead.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        Dead.start();
        Dead.release();
        transform.position = respawnPoint.transform.position;
    }

    public float GetFlaskUses() {

        return flaskUses;
    }

}


