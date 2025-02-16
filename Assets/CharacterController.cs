using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main Authour : Tim Ag�lii
public class CharacterController : MonoBehaviour
{

    [SerializeField]
    float moveSpeed;


    [SerializeField]
    private int maxMana;
    private int mana = 0;

    [SerializeField]
    private int bowManaCost;
    [SerializeField]
    private int heavyManaCost;


    float moveSpeedDefault;

    private Rigidbody playerRgb;

    float attackTimer = 0;

    float dodgeTimer = 0;

    float flaskTimer = 0;

    Vector3 rotationOffset = new Vector3(90, 0, 0);

    Quaternion lookRotation;


    private Plane plane;

    bool moveAllow = true;

    bool invincibility = false;


    //healthFlask
    bool healthFlaskTimerRunning = true;
    bool healthFlaskStart = false;
    bool healthFlaskOfCooldown = true;

    float flaskUses = 2;
    float healthFlaskSpeedFactor = 0.3f;
    float healthFlaskDuration = 1f;
    float healthFlaskCooldown = 0.5f;

    //attack
    bool attacking = false;

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

    private bool playerInputActive = false;

    Vector3 playerMovement;

    private bool canMove = true;
  

    [SerializeField]
    private float dodgeDropOffTime;
    //hitbox variables

    [SerializeField]
    private GameObject lightAttackHitbox;

    private GameObject attackHitbox;
    
    [SerializeField]
    private GameObject heavyAttackHitbox;

    [SerializeField]
    private float timeToNextSwing;
   
    [SerializeField]
    private float timeToNextSwingLight;
    [SerializeField]
    private float timeToNextSwingHeavy;

    private bool startAttackDelay = false;
    
    [SerializeField]
    private float lightAttackDelay;


    private bool endOfAttack = false;

    private float attackDelay;



    [SerializeField]
    private float lightSwingCooldown = 0.6f;

    private float swingCooldown;

    [SerializeField]
    private float heavySwingCooldown;
    [SerializeField]
    private float heavyAttackDelay;


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
    int attackComboLenght = 3;
    int currentAttack = 1;
    string currentAttackTrigger;

    public Transform respawnPoint;

    private FMOD.Studio.EventInstance HealthRefill;
    private FMOD.Studio.EventInstance Dead;


    Vector3 forward, right;


    [SerializeField]
    private GameObject bow;
    [SerializeField]
    private GameObject sword;

    [SerializeField]
    private float bowChargeTime;
    [SerializeField]
    private GameObject arrow;
    private bool bowIsLoading = false;
    private bool bowIsFinishedLoading = false;
    private bool startBowCooldown = false;
    private bool drawBow = false;
    [SerializeField]
    private float bowCooldownTime;
    [SerializeField]
    private float bowDrawTime;
    private bool queueBowCancel = false;
    private bool bowIsActive = false;
   


    // Start is called before the first frame update
    void Start()
    {
      
        playerRgb = transform.GetComponent<Rigidbody>();
      
        bow.SetActive(false);
        canMove = true;
        plane = new Plane(Vector3.up, Vector3.zero);
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        moveSpeedDefault = moveSpeed;

        transform.GetComponentInParent<PlayerAnimEvents>().SetPlayerMoveSpeed(moveSpeedDefault);

        currentAttackTrigger = "Attack1";
     //  gameObject.GetComponent<ArrowUI>().UpdateAmmo(mana, maxMana);
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


            AttackManager();

            BowManager();


            DodgerollManager();



            EquipManager();

            HealthFlaskManager();


            UpdateEventVariables();

       
            PlayerRotationUpdate();

            UpdateMoveInput();

          


            //anim stuff here. 
            anim.SetFloat("XSpeed", Input.GetAxis("HorizontalKey"));
            anim.SetFloat("YSpeed", Input.GetAxis("VerticalKey"));
        }
    }
    private void FixedUpdate()
    {
      

    }

    private void PlayerRotationUpdate()
    {

        if (moveAllow && Mathf.Abs(Input.GetAxis("HorizontalKey")) + Mathf.Abs(Input.GetAxis("VerticalKey")) != 0)
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

    private void UpdateEventVariables()
    {
       moveSpeed = transform.GetComponentInParent<PlayerAnimEvents>().GetPlayerMoveSpeed();
       moveAllow = transform.GetComponentInParent<PlayerAnimEvents>().GetAllowMovement();
     

    }

   private void UpdateMoveInput() {
        Vector3 rightMovement = right * moveSpeed *  Input.GetAxis("HorizontalKey");
        Vector3 upMovement = forward * moveSpeed * Input.GetAxis("VerticalKey");

        playerMovement = rightMovement + upMovement;

        inputDirection = playerMovement.normalized;
      


        if (playerMovement.magnitude > moveSpeed)
        {
            playerMovement = playerMovement.normalized * moveSpeed;
        }

        if (moveAllow == true)
        {
            Move();
        }

        if (playerMovement.magnitude == 0)
        {
            playerInputActive = false;
        }
        else {
            playerInputActive = true;
        }


      
    }

    
    
   private void Move()
    {
     
        playerRgb.velocity = playerMovement + new Vector3(0, playerRgb.velocity.y,0);
   
  
    }

    public void ManaIncreased(int i) {
        mana = mana + i;
        if (mana > maxMana) {
            mana = maxMana;
        }
        //  gameObject.GetComponent<ArrowUI>().UpdateAmmo(mana, maxMana);
    }

    public int GetMaxMana() {
        return maxMana;
    }

    public int GetMana()
    {
        return mana;
    }


   private void EquipManager() {
        if (bowIsActive == false && startAttackDelay == false && attacking == false)
        {
       
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                bow.SetActive(true);
                sword.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                bow.SetActive(false);
                sword.SetActive(true);
            }

        }

    }

   private void BowManager() {
       if (bow.activeSelf == true && dodgerollTimerRunning == false && healthFlaskStart == false)
        {
            if (bowIsActive == false && mana >= bowManaCost) {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    StartBowDraw();

                }

            }

            if (bowIsActive == true)
            {
           

                if (drawBow)
                {
                    transform.rotation = lookRotation;
                    DrawBow();
                }

                if (bowIsLoading)
                {
                    transform.rotation = lookRotation;

                    BowLoading();

                }
                if (bowIsFinishedLoading)
                {

                    transform.rotation = lookRotation;
                }

                if (Input.GetKeyUp(KeyCode.Mouse0) && bowIsFinishedLoading == true)
                {

                    BowFire();
                }

                if (startBowCooldown)
                {
                    BowCooldown();

                }

                if ((Input.GetKeyUp(KeyCode.Mouse0) && drawBow == false && bowIsFinishedLoading == false && startBowCooldown == false && drawBow == false) || drawBow == false && queueBowCancel == true)
                {

                    BowCancel();
                }
            }
        }


   

    }
   private void StartBowDraw() {

        bowIsActive = true;
            anim.SetTrigger("DrawBow");
        playerRgb.velocity = new Vector3(0, playerRgb.velocity.y, 0);
        transform.GetComponentInParent<PlayerAnimEvents>().SetAllowMovementFalse();

        drawBow = true;
        
    }
   private void DrawBow() {
        if (AttackWaitTimer(bowDrawTime))
        {
            
            
            drawBow = false;

            if (queueBowCancel == false) {
                bowIsLoading = true;
            anim.SetTrigger("StopBow");
            anim.SetTrigger("BowAim");
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Mouse0)){
                queueBowCancel = true;
            }
          
        }

    }

   private void BowLoading()
    {
      

        if (AttackWaitTimer(bowChargeTime))
        {
            bowIsFinishedLoading = true;
            bowIsLoading = false;
        }
        else
        {
           
        }
    }

   private void BowFire() {
      
        mana = mana - bowManaCost;
        Debug.Log(mana + "  manaleft");
     //   gameObject.GetComponent<ArrowUI>().UpdateAmmo(mana, maxMana);
     bowIsFinishedLoading = false;
     anim.SetTrigger("StopBow");
     anim.SetTrigger("BowRecoil");

        startBowCooldown = true;
     
    }
   private void BowCancel() {
        anim.SetTrigger("StopBow");
        transform.GetComponentInParent<PlayerAnimEvents>().SetAllowMovementTrue();
        queueBowCancel = false;
        attackTimer = 0;
        drawBow = false;
        bowIsLoading = false;
        bowIsFinishedLoading = false;
        startBowCooldown = false;

        bowIsActive = false;
     
    }

    

   private void BowCooldown() {
        if (AttackWaitTimer(bowCooldownTime))
        {
            transform.GetComponentInParent<PlayerAnimEvents>().SetAllowMovementTrue();
            startBowCooldown = false;
            bowIsActive = false;
            anim.SetTrigger("StopBow");

        }
        else { 
        
        }
    }



   private void HealthFlaskManager()
    {
        if (Input.GetKeyDown(KeyCode.Q) && healthFlaskOfCooldown && flaskUses > 0 && startAttackDelay == false && attacking == false && dodgerolling == false && bowIsActive == false && gameObject.GetComponent<PlayerHealthScript>().GetHealth() < gameObject.GetComponent<PlayerHealthScript>().GetMaxHealth())
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

                    GetComponentInParent<PlayerHealthScript>().regainHealth(2);
                  
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

   private void StartDodgeroll() {
        
            dodgerollStart = true;
            dodgerollTimerRunning = true;

            //more anim things
            //Debug.Log("in player Dodgeroll");
            anim.SetTrigger("DodgeRoll");



        transform.GetComponentInParent<PlayerAnimEvents>().SetAllowMovementFalse();

        if (playerInputActive)
        {
            transform.rotation = Quaternion.LookRotation(inputDirection);
        }
        
  

        dodgerolling = true;
        dodgerollOfCooldown = false;
        invincibility = true;
    }

   private void DodgerollManager()
    {
    if (Input.GetKeyDown(KeyCode.Space) && dodgerollOfCooldown && startAttackDelay == false && attacking == false && bowIsActive == false)
    {
        StartDodgeroll();
        }
        if (dodgerollStart == true)
        {

            if (dodgerollTimerRunning == true)
            {
                if (DodgeWaitTimer(dodgerollDuration))
                {

                    transform.GetComponentInParent<PlayerAnimEvents>().SetAllowMovementTrue();
                    dodgerolling = false;
                    dodgerollTimerRunning = false;
                   
                }
                else
                {
                    if (dodgeTimer < dodgeDropOffTime)
                    {
                        playerRgb.velocity = ((transform.forward).normalized * dodgerollSpeed ) +new Vector3(0, playerRgb.velocity.y, 0); ;
                    }
                    else {
                        invincibility = false;
                        playerRgb.velocity = ((transform.forward).normalized * dodgerollDropSpeed) + new Vector3 (0,playerRgb.velocity.y,0);
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

   private void AttackManager()
    {
        endOfAttack = transform.GetComponentInParent<PlayerAnimEvents>().GetEndOfAttack();

        InAttack();

        if (sword.activeSelf == true && attacking == false && dodgerollTimerRunning == false && healthFlaskStart == false) 
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                currentAttack = 1;
         
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.Mouse1) && mana >= heavyManaCost)
            {
                mana = mana - heavyManaCost;

                HeavyAttack();
            }

        }

       
    }

   private void Attack()
    {
  
        //more anim things
        //Debug.Log("in player attack");
      
        currentAttackTrigger = "Attack" + currentAttack;
        anim.SetTrigger(currentAttackTrigger);


        playerRgb.velocity = new Vector3(0, playerRgb.velocity.y, 0);


        transform.GetComponentInParent<PlayerAnimEvents>().SetEndOfAttackFalse();
        transform.GetComponentInParent<PlayerAnimEvents>().SetAllowMovementFalse();
       
        transform.rotation = lookRotation;
        attacking = true;

       

    }

   private void HeavyAttack() {

        anim.SetTrigger("HeavyAttack");

        transform.GetComponentInParent<PlayerAnimEvents>().SetAllowMovementFalse();
        transform.rotation = lookRotation;
        attacking = true;

    }

   
    private void InAttack()
    {
   
        if (attacking)
        {
          
           
            if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                 
                    queueAttack = true;
                    queueDodge = false;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                   
                    queueAttack = false;
                    queueDodge = true;
                }
         
            if (endOfAttack == false)
            {
                playerRgb.velocity = ((transform.forward).normalized * 2f) + new Vector3(0, playerRgb.velocity.y, 0);
            }


            if (endOfAttack == true)
            {
                playerRgb.velocity = new Vector3(0, playerRgb.velocity.y, 0);

                if (AttackWaitTimer(lightSwingCooldown)) {
                     anim.SetTrigger("StopAttack");
                    attacking = false;
                    transform.GetComponentInParent<PlayerAnimEvents>().SetAllowMovementTrue();
                    transform.GetComponentInParent<PlayerAnimEvents>().SetEndOfAttackFalse();
                }


                if (queueAttack == true)
                {
                    currentAttack++;
                    queueAttack = false;
                    if (currentAttack <= 3){

                        anim.SetTrigger("StopAttack");
                        attacking = false;
                        transform.GetComponentInParent<PlayerAnimEvents>().SetEndOfAttackFalse();
                        transform.GetComponentInParent<PlayerAnimEvents>().SetAllowMovementTrue();
                        attackTimer = 0;
                        Attack();
                    }
                }
             
                if (queueDodge == true)
                {
                    queueDodge = false;
                    anim.SetTrigger("StopAttack");
                    attacking = false;
                    transform.GetComponentInParent<PlayerAnimEvents>().SetEndOfAttackFalse();
                    transform.GetComponentInParent<PlayerAnimEvents>().SetAllowMovementTrue();
                    attackTimer = 0;
                    StartDodgeroll();
               
                }

              
                


            }

            }

               
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
        GetComponent<PlayerHealthScript>().regainHealth(100);
        GetComponent<PlayerHealthScript>().ResetPotions();
        Dead = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Player/Dead");
        Dead.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        Dead.start();
        Dead.release();
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        transform.position = respawnPoint.transform.position;
    }

    public void SetRespawnPoint(Vector3 position)
    {
        //Debug.Log("Respawnpoint Set");
        respawnPoint.transform.position = new Vector3(position.x, position.y + 2f, position.z);
    }

   

    public float GetFlaskUses() {

        return flaskUses;
    }



    public void SetFlaskUses(int x)
    {
        flaskUses = x;
    }

}


