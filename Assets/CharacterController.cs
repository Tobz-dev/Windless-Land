using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Main Authour : Tim Ag�lii
public class CharacterController : MonoBehaviour
{

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    GameObject fadeToBlack;

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

    private bool moveAllow = true;

    private bool invincibility = false;

    private bool endPlayerStunned = false;
    private bool startPlayerStunned = false;

    private bool resetAnim = false;


    //accesibility input delay
    private bool inputDelayOn = false;
    private float  inputDelay = 0.5f;
    private float delayTimer = 0;
    private bool allowNextInput = false;
    private bool runTimerOnce = true;

    //healthFlask
    private bool doneDrinkingFlask = false;
    private bool usingHealthFlask = false;
    private bool healthFlaskOfCooldown = true;


    private float flaskUses = 2;

    private int originalFlaskUsesAmount;

    private float healthFlaskCooldown = 0.5f;

    //attack
    private bool attacking = false;

    //dodgeroll
    [SerializeField]
    private float dodgerollDuration = 0.7f;
    [SerializeField]
    private float dodgerollCooldown = 0.2f;


    [SerializeField]
    private float dodgerollSpeed = 11f;
    [SerializeField]
    private float dodgerollDropSpeed = 3f;
    private bool dodgerollTimerRunning = false;
    private bool dodgerollStart = false;
    private bool dodgerolling = false;
    private bool dodgerollOfCooldown = true;
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


    //henrik prototyp
    [SerializeField]
    private Transform objectToFace;
    private GameObject closestEnemy;
    public bool autoAim = false;
    Quaternion enemyLookRotation;


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
    private PlayerVFX playerVFX;

    int attackComboLenght = 3;
    int currentAttack = 1;
    string currentAttackTrigger;

    public Transform respawnPoint;

    private FMOD.Studio.EventInstance HealthRefill;
    private FMOD.Studio.EventInstance Dead;


    Vector3 forward, right;

    //equips
    [SerializeField]
    private GameObject bow;
    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private GameObject healthPot;

    private bool swordEquipped = true;

    private bool bowEquipped = false;

 

    //bow
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

    //control rebinding
    PlayerInputs inputActions;
    Vector2 movementInput; //player walk rebindings
    private bool attackActivated; //will probably be needed to get bow to work for gamepad
    private bool attackDone;
    private bool attackCanceled;
    private bool keyboardUsed;
    private bool mouseUsed;
    private bool gamepadUsed;
    private string controlUsed;
    private string movePath;

    private void OnEnable()
    {
        inputActions.WindlessLand.Enable();
        inputActions.WindlessLand.Move.performed += MovePerformed;
        inputActions.WindlessLand.Attack.started += ctx => attackActivated = true;
        inputActions.WindlessLand.Attack.performed += ctx => attackDone = true;
        inputActions.WindlessLand.Attack.canceled += ctx => attackCanceled = true;
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Awake()
    {
        inputActions = InputManager.inputActions;
    }

    private void MovePerformed(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>(); //saves which direction character is moving in
        controlUsed = ctx.control.path; //saves if player is using keyboard or gamepad
    }

    // Start is called before the first frame update
    void Start()
    {
        originalFlaskUsesAmount = (int)flaskUses;
        playerRgb = transform.GetComponent<Rigidbody>();
        playerVFX = GetComponent<PlayerVFX>();
        swordEquipped = true;
        bowEquipped = false;
        bow.SetActive(false);
        healthPot.SetActive(false);
        canMove = true;
        plane = new Plane(Vector3.up, Vector3.zero);
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        moveSpeedDefault = moveSpeed;
        transform.GetComponentInParent<PlayerAnimEvents>().SetPlayerMoveSpeedFactor(1);


        currentAttackTrigger = "Attack1";
        //  gameObject.GetComponent<ArrowUI>().UpdateAmmo(mana, maxMana);

        updateAutoaim();
        controlUsed = "Mouse";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Respawn();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(closestEnemy);
            closestEnemy = FindClosestEnemy();
            objectToFace = closestEnemy.transform;
            Debug.Log(closestEnemy);
            transform.LookAt(objectToFace);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float enter) && canMove == true)
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            plane.SetNormalAndPosition(Vector3.up, transform.position);
            Vector3 playerPositionOnPlane = plane.ClosestPointOnPlane(transform.position);

            lookRotation = Quaternion.LookRotation(hitPoint - playerPositionOnPlane);

            StunHandler();

            if (startPlayerStunned == false) 
            {
                AttackManager();


                BowManager();


                DodgerollManager();
              
                HealthFlaskManager();

            }

            EquipManager();



            UpdateEventVariables();

       
            PlayerRotationUpdate();

            UpdateMoveInput();





            //anim stuff here. 
            anim.SetFloat("XSpeed", movementInput.x);//Input.GetAxis("HorizontalKey"));
            anim.SetFloat("YSpeed", movementInput.y);//Input.GetAxis("VerticalKey"));
        }


        //if(attacking == true)
        //{
        //    playerRgb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        //}
        //else { playerRgb.constraints &= ~RigidbodyConstraints.FreezePositionY; }
    }

    private void PlayerRotationUpdate()
    {

        if (moveAllow && (Mathf.Abs(movementInput.x) + Mathf.Abs(movementInput.y)) != 0)//Mathf.Abs(Input.GetAxis("HorizontalKey")) + Mathf.Abs(Input.GetAxis("VerticalKey")) != 0)
        {
            Vector3 horizontal = movementInput.x * right; //(Input.GetAxis("Horizontal") * right);
            Vector3 vertical = movementInput.y * forward; //(Input.GetAxis("Vertical") * forward);

            //sets horizontal to Input.GetAxis("Horizontal") if chosen rebinding is a button that triggers it
            if ((inputActions.WindlessLand.Move.bindings[3].effectivePath.Equals("<Keyboard>/a") || inputActions.WindlessLand.Move.bindings[3].effectivePath.Equals("<Keyboard>/leftArrow")) && (inputActions.WindlessLand.Move.bindings[4].effectivePath.Equals("<Keyboard>/d") || inputActions.WindlessLand.Move.bindings[4].effectivePath.Equals("<Keyboard>/rightArrow")) && !controlUsed.Contains("Gamepad"))
            {
                horizontal = (Input.GetAxis("Horizontal") * right);
            }

            //sets vertical to Input.GetAxis("Vertical") if chosen rebinding is a button that triggers it
            //control rebinding & smooth turns do not go well together :blobsad:
            if ((inputActions.WindlessLand.Move.bindings[1].effectivePath.Equals("<Keyboard>/w") || inputActions.WindlessLand.Move.bindings[1].effectivePath.Equals("<Keyboard>/upArrow")) && (inputActions.WindlessLand.Move.bindings[2].effectivePath.Equals("<Keyboard>/s") || inputActions.WindlessLand.Move.bindings[2].effectivePath.Equals("<Keyboard>/downArrow")) && !controlUsed.Contains("Gamepad"))
            {
                vertical = (Input.GetAxis("Vertical") * forward);
            }

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
       moveSpeed = moveSpeedDefault * GetComponentInParent<PlayerAnimEvents>().GetPlayerMoveSpeedFactor();
       moveAllow = GetComponentInParent<PlayerAnimEvents>().GetAllowMovement();
       endPlayerStunned = GetComponentInParent<PlayerAnimEvents>().GetEndPlayerStunned();
        doneDrinkingFlask = GetComponentInParent<PlayerAnimEvents>().GetDoneDrinkingPot();
    }

   private void UpdateMoveInput() 
   {
        Vector3 rightMovement = right * moveSpeed * movementInput.x; //Input.GetAxis("HorizontalKey");
        Vector3 upMovement = forward * moveSpeed * movementInput.y; //Input.GetAxis("VerticalKey");

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
        else 
        {
            playerInputActive = true;
        }


      
    }

    
    
   private void Move()
    {
     
        playerRgb.velocity = playerMovement + new Vector3(0, playerRgb.velocity.y,0);
   
  
    }

    public void ManaIncreased(int i) 
    {
        mana = mana + i;
        if (mana > maxMana) 
        {
            mana = maxMana;
        }
        //  gameObject.GetComponent<ArrowUI>().UpdateAmmo(mana, maxMana);
    }

    public int GetMaxMana() 
    {
        return maxMana;
    }

    public void SetMana(int x)
    {
        mana = x;
    }

    public int GetMana()
    {
        return mana;
    }


   private void EquipManager() 
   {
        if (bowIsActive == false && startAttackDelay == false && attacking == false && moveAllow == true && usingHealthFlask == false)
        {
            //detects when Equip Bow rebinding is triggered
            if (inputActions.WindlessLand.EquipBow.triggered)//Input.GetKeyDown(KeyCode.Alpha2))
            {
                bow.SetActive(true);
                sword.SetActive(false);
                swordEquipped = false;
                bowEquipped = true;
            }
            //detects when Equip Sword rebinding is triggered
            if (inputActions.WindlessLand.EquipSword.triggered)//Input.GetKeyDown(KeyCode.Alpha1))
            {
                bow.SetActive(false);
                sword.SetActive(true);
                bowEquipped = false;
                swordEquipped = true;
            }

        }

    }

   private void BowManager() 
    {
        //long annoying code for checking & converting binding.effectivePath 
        //to a format that works with Input.GetKeyDown etc etc
        //i'm sure there are better ways to do this but it was the first one that worked after lots of testing & googling
        string keyboardPath = inputActions.WindlessLand.Attack.bindings[0].effectivePath;
        string mousePath = inputActions.WindlessLand.Attack.bindings[1].effectivePath;
        int pathNameIndex = keyboardPath.IndexOf('/') + 1;
        int pathNameIndexMouse = mousePath.IndexOf('/') + 1;
        if (mousePath.Equals("<Mouse>/leftButton") || mousePath.Equals("<Mouse>/middleButton") || mousePath.Equals("<Mouse>/rightButton"))
        {
            if (mousePath.Equals("<Mouse>/leftButton"))
            {
                mousePath = "mouse 0";
            }
            else if (keyboardPath.Equals("<Mouse>/middleButton"))
            {
                keyboardPath = "mouse 1";
            }
            else if (keyboardPath.Equals("<Mouse>/rightButton"))
            {
                keyboardPath = "mouse 2";
            }
            pathNameIndexMouse = 0;
        }

        if (keyboardPath.Equals("<Mouse>/leftButton") || keyboardPath.Equals("<Mouse>/middleButton") || keyboardPath.Equals("<Mouse>/rightButton"))
        {
            if (keyboardPath.Equals("<Mouse>/leftButton"))
            {
                keyboardPath = "mouse 0";
            }
            else if (keyboardPath.Equals("<Mouse>/middleButton"))
            {
                keyboardPath = "mouse 1";
            }
            else if (keyboardPath.Equals("<Mouse>/rightButton"))
            {
                keyboardPath = "mouse 2";
            }
            pathNameIndex = 0;
        }
        keyboardPath = keyboardPath.Substring(pathNameIndex);
        mousePath = mousePath.Substring(pathNameIndexMouse);

        if (bow.activeSelf == true && dodgerollTimerRunning == false && usingHealthFlask == false && startPlayerStunned == false)
        {
            if (bowIsActive == false && mana >= bowManaCost) 
            {
                if (Input.GetKeyDown(keyboardPath))//KeyCode.Mouse0))
                {
                    StartBowDraw();
                    keyboardUsed = true;
                    mouseUsed = false;
                    gamepadUsed = false;
                }

                if (Input.GetKeyDown(mousePath))
                {
                    StartBowDraw();
                    mouseUsed = true;
                    keyboardUsed = false;
                    gamepadUsed = false;
                }
            }

            if (bowIsActive == true)
            {
           

                if (drawBow)
                {
                    if (autoAim == true && FindClosestEnemy() != null)
                    {
                        closestEnemy = FindClosestEnemy();
                        objectToFace = closestEnemy.transform;
                        transform.LookAt(objectToFace);
                    }


                    else
                    {
                        transform.rotation = lookRotation;
                    }
                    DrawBow(keyboardPath, mousePath);
                }

                if (bowIsLoading)
                {
                    if (autoAim == true && FindClosestEnemy() != null)
                    {
                        closestEnemy = FindClosestEnemy();
                        objectToFace = closestEnemy.transform;
                        transform.LookAt(objectToFace);
                    }


                    else
                    {
                        transform.rotation = lookRotation;
                    }

                    BowLoading();

                }
                if (bowIsFinishedLoading)
                {

                    if (autoAim == true && FindClosestEnemy() != null)
                    {
                        closestEnemy = FindClosestEnemy();
                        objectToFace = closestEnemy.transform;
                        transform.LookAt(objectToFace);
                    }


                    else
                    {
                        transform.rotation = lookRotation;
                    }
                }

                if (((Input.GetKeyUp(keyboardPath) && keyboardUsed) || (Input.GetKeyUp(mousePath) && !keyboardUsed)/*Input.GetKeyUp(KeyCode.Mouse0)*/ && bowIsFinishedLoading == true))
                {

                    BowFire();
                }

                if (startBowCooldown)
                {
                    BowCooldown();

                }

                if ((((Input.GetKeyUp(keyboardPath) && keyboardUsed) || (Input.GetKeyUp(mousePath) && !keyboardUsed) /*Input.GetKeyUp(KeyCode.Mouse0)*/ && drawBow == false && bowIsFinishedLoading == false && startBowCooldown == false && drawBow == false) || drawBow == false && queueBowCancel == true))
                {

                    BowCancel();
                }
            }
        }


   

    }
   private void StartBowDraw() 
   {

        bowIsActive = true;
        anim.SetTrigger("DrawBow");
        playerVFX.PlayArrowChannelEffect();
        playerRgb.velocity = new Vector3(0, playerRgb.velocity.y, 0);
        transform.GetComponentInParent<PlayerAnimEvents>().SetAllowMovementFalse();

        drawBow = true;
        
    }
   private void DrawBow(string keyboardPath, string mousePath) 
   {
        if (AttackWaitTimer(bowDrawTime))
        {
            
            
            drawBow = false;

            if (queueBowCancel == false) 
            {
                bowIsLoading = true;
                anim.SetTrigger("StopBow");
                anim.SetTrigger("BowAim");
            }
        }
        else
        {
            string path = mousePath;
            if (keyboardUsed)
            {
                path = keyboardPath;
            }
            if (Input.GetKeyUp(path))//Input.GetKeyUp(KeyCode.Mouse0))
            {
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

   private void BowFire() 
   {
      
        mana = mana - bowManaCost;
        Debug.Log(mana + "  manaleft");
     //   gameObject.GetComponent<ArrowUI>().UpdateAmmo(mana, maxMana);
     bowIsFinishedLoading = false;
     anim.SetTrigger("StopBow");
     anim.SetTrigger("BowRecoil");
        playerVFX.StopArrowChannelEffect();
        startBowCooldown = true;
     
   }
   private void BowCancel() 
   {
        anim.SetTrigger("StopBow");
        playerVFX.StopArrowChannelEffect();
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
        if (inputActions.WindlessLand.HealthRefill.triggered/*Input.GetKeyDown(KeyCode.Q)*/ && usingHealthFlask == false && healthFlaskOfCooldown && flaskUses > 0 && attacking == false && dodgerollTimerRunning == false && bowIsActive == false && gameObject.GetComponent<PlayerHealthScript>().GetHealth() < gameObject.GetComponent<PlayerHealthScript>().GetMaxHealth() && moveAllow == true)
        {
            anim.SetBool("DrinkingPot",true);
            playerVFX.PlayPotionEffect();
            
            HealthRefill = FMODUnity.RuntimeManager.CreateInstance("event:/Game/HealthRefill");
            HealthRefill.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            HealthRefill.start();
            HealthRefill.release();
            usingHealthFlask = true;
            healthPot.SetActive(true);

            if (bowEquipped)
            {
                bow.SetActive(false);
            }
            if (swordEquipped)
            {
                sword.SetActive(false);
            }
        }

        if (doneDrinkingFlask == true) 
        {
            GetComponentInParent<PlayerHealthScript>().regainHealth(2);
            GetComponentInParent<PlayerAnimEvents>().SetPlayerMoveSpeedFactor(1);
            flaskUses--;
            anim.SetBool("DrinkingPot", false);
            healthFlaskOfCooldown = false;
            usingHealthFlask = false;
            GetComponentInParent<PlayerAnimEvents>().SetDoneDrinkingPotFalse();

            healthPot.SetActive(false);
            if (bowEquipped)
            {
                bow.SetActive(true);
            }
            if (swordEquipped)
            {
                sword.SetActive(true);
            }
        }

        if (healthFlaskOfCooldown == false)
        {
            if (FlaskWaitTimer(healthFlaskCooldown))
            {
                
    
                healthFlaskOfCooldown = true;
              
                

            }
        }

        if (dodgerollStart == true)
            HealthFlaskCancel();
   }


    private void HealthFlaskCancel() {
        anim.SetBool("DrinkingPot", false);
        GetComponentInParent<PlayerAnimEvents>().SetPlayerMoveSpeedFactor(1);
        healthFlaskOfCooldown = true;
        usingHealthFlask = false;
        flaskTimer = 0;
        healthPot.SetActive(false);
        if (bowEquipped) {
            bow.SetActive(true);
        }
        if (swordEquipped)
        {
            sword.SetActive(true);
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
    if (inputActions.WindlessLand.Dodgeroll.triggered/*Input.GetKeyDown(KeyCode.Space)*/ && dodgerollOfCooldown && attacking == false && bowIsActive == false && moveAllow == true)
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

            if (sword.activeSelf == true && attacking == false && dodgerollTimerRunning == false && usingHealthFlask == false && moveAllow == true)
            {
                if (inputActions.WindlessLand.Attack.triggered/*Input.GetKeyDown(KeyCode.Mouse0)*/)
                {
                    currentAttack = 1;

                    Attack();
                }
                if (inputActions.WindlessLand.HeavyAttack.triggered/*Input.GetKeyDown(KeyCode.Mouse1)*/ && mana >= heavyManaCost)
                {
                    mana = mana - heavyManaCost;

                    HeavyAttack();
                }

            }
        
       
    }

   private void Attack()
    {

        if (inputDelayOn) {
            delayTimer = 0;
            allowNextInput = false;
            runTimerOnce = true;
        }

        //more anim things
        //Debug.Log("in player attack");

        currentAttackTrigger = "Attack" + currentAttack;
        anim.SetTrigger(currentAttackTrigger);
        playerVFX.PlayLightAttackEffect();

        playerRgb.velocity = new Vector3(0, playerRgb.velocity.y, 0);


        transform.GetComponentInParent<PlayerAnimEvents>().SetEndOfAttackFalse();
        transform.GetComponentInParent<PlayerAnimEvents>().SetAllowMovementFalse();


        //Vector3 hitPoint = ray.GetPoint(enter);
        //plane.SetNormalAndPosition(Vector3.up, transform.position);
        //Vector3 playerPositionOnPlane = plane.ClosestPointOnPlane(transform.position);

        //lookRotation = Quaternion.LookRotation(hitPoint - playerPositionOnPlane);


        if (autoAim == true && FindClosestEnemy() != null)
        {
            closestEnemy = FindClosestEnemy();
            objectToFace = closestEnemy.transform;


            // Vector3 hitPoint2 = closestEnemy.transform.position;
            // Vector3 playerPositionOnPlane2 = plane.ClosestPointOnPlane(transform.position);
            // enemyLookRotation = Quaternion.LookRotation(hitPoint2 - playerPositionOnPlane2);
            // transform.rotation = enemyLookRotation;
            
            
            transform.LookAt(objectToFace);
        }


        else
        {
            transform.rotation = lookRotation;
        }


        attacking = true;
        playerRgb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;



    }

   private void HeavyAttack() {

        anim.SetTrigger("HeavyAttack");
        playerVFX.PlayHeavyAttackEffect();

        transform.GetComponentInParent<PlayerAnimEvents>().SetAllowMovementFalse();
        if (autoAim == true && FindClosestEnemy() != null)
        {
            closestEnemy = FindClosestEnemy();
            objectToFace = closestEnemy.transform;
            transform.LookAt(objectToFace);
        }


        else
        {
            transform.rotation = lookRotation;
        }
        attacking = true;
        playerRgb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;

    }

   
    private void InAttack()
    {
       

        if (attacking && endPlayerStunned == false && usingHealthFlask == false && dodgerolling == false)
        {

            if (inputDelayOn == false)
            {

                if (inputActions.WindlessLand.Attack.triggered)//Input.GetKeyDown(KeyCode.Mouse0))
                {

                    queueAttack = true;
                    queueDodge = false;
                }
                if (inputActions.WindlessLand.Dodgeroll.triggered)//Input.GetKeyDown(KeyCode.Space))
                {

                    queueAttack = false;
                    queueDodge = true;
                }
            } else if (inputDelayOn)
            {

                if (runTimerOnce == true) 
                { 
                
              
                if (InputDelayTimer())
                {
                    allowNextInput = true;
                    runTimerOnce = false;

                }
                }

                if (allowNextInput == true) 
                { 

                if (inputActions.WindlessLand.Attack.triggered)//Input.GetKeyDown(KeyCode.Mouse0))
                {

                    queueAttack = true;
                    queueDodge = false;
                }
                if (inputActions.WindlessLand.Dodgeroll.triggered)//Input.GetKeyDown(KeyCode.Space))
                {

                    queueAttack = false;
                    queueDodge = true;
                }
                }
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
                    playerRgb.constraints &= ~RigidbodyConstraints.FreezePositionY;
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
                        playerRgb.constraints &= ~RigidbodyConstraints.FreezePositionY;
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
                    playerRgb.constraints &= ~RigidbodyConstraints.FreezePositionY;
                    transform.GetComponentInParent<PlayerAnimEvents>().SetEndOfAttackFalse();
                    transform.GetComponentInParent<PlayerAnimEvents>().SetAllowMovementTrue();
                    attackTimer = 0;
                    StartDodgeroll();
               
                }

              
                


            }

            }

               
                }

    void AttackCancel() {
        if (attacking == true)
        {
            anim.SetTrigger("StopAttack");
        }


            attacking = false;
        playerRgb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        attackTimer = 0;
            transform.GetComponentInParent<PlayerAnimEvents>().SetAllowMovementTrue();
            transform.GetComponentInParent<PlayerAnimEvents>().SetEndOfAttackFalse();
            queueDodge = false;
            queueAttack = false;
          


    }



    private void StunHandler()
    {

        if (startPlayerStunned == true)
        {
            if (resetAnim == false)
            {
                anim.SetBool("PlayerIsStunned", true);
            }
            resetAnim = false;

            playerRgb.velocity = (-(transform.forward).normalized * 1.5f) + new Vector3(0, playerRgb.velocity.y, 0);

            if (endPlayerStunned == true)
            {
                anim.SetBool("PlayerIsStunned", false);
                transform.GetComponentInParent<PlayerAnimEvents>().SetEndPlayerStunnedFalse();


                if (attacking == true)
                {
                    AttackCancel();
                }
                if (bowIsActive == true)
                {
                    BowCancel();
                }
                if (usingHealthFlask == true)
                {
                    HealthFlaskCancel();
                }
                startPlayerStunned = false;
                transform.GetComponentInParent<PlayerAnimEvents>().SetAllowMovementTrue();
            }

        }

    }

        public void StartPlayerStun()
    {

        AttackCancel();

        if (bowIsActive == true)
        {
            BowCancel();
        }
        if (usingHealthFlask == true)
        {
            HealthFlaskCancel();
        }
        if (startPlayerStunned == true)
        {
            ResetStunAnim();
        }
        CancelLeverPull();


        startPlayerStunned = true;



    }
    private void ResetStunAnim()
    {
        anim.SetBool("PlayerIsStunned", false);
        resetAnim = true;
    }
  
   
    
    public void PullLever() {
        if (moveAllow == true && attacking == false && usingHealthFlask == false && bowIsActive == false && dodgerollTimerRunning == false) {
            anim.SetBool("PullingLever", true);
        }
      
    }

    public void CancelLeverPull() {
        if(anim.GetBool("PullingLever") == true)
        anim.SetBool("PullingLever", false);
        
        transform.GetComponentInParent<PlayerAnimEvents>().SetAllowMovementTrue();
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
        fadeToBlack.SetActive(true);
        AttackCancel();
        GetComponentInParent<PlayerAnimEvents>().SetAllowMovementFalse();
        playerRgb.velocity = new Vector3(0, 0, 0);
        fadeToBlack.GetComponent<FadeToBlack>().ActivateBlackScreen();
        playerRgb.constraints = RigidbodyConstraints.FreezePosition;
        invincibility = true;
        

        
        Debug.Log("Player Dead"); 
        anim.SetBool("Dying", true);
        Dead = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Player/Dead");
        Dead.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        Dead.start();
        Dead.release();
        StartCoroutine(TrueRespawn());
        GetComponent<PlayerHealthScript>().regainHealth(100);


    }

    private IEnumerator TrueRespawn()
    {
       
        yield return new WaitForSeconds(5.0f);
        GetComponentInParent<PlayerAnimEvents>().SetAllowMovementTrue();
        playerRgb.constraints = RigidbodyConstraints.None;
        playerRgb.constraints = RigidbodyConstraints.FreezeRotation;
        anim.SetBool("Dying", false);
        ResetPotionsToOriginal();
        SetMana(0);
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (go.name != "Boss")
            {
                Destroy(go);
            }
        }


        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Respawner"))
        {

            go.GetComponent<EnemyRespawnScript>().RespawnEnemy();

        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("ManaFountain"))
        {

            go.GetComponentInChildren<ManaFountain>().RestoreFountain();

        }

        playerRgb.velocity = new Vector3(0, 0, 0);
        fadeToBlack.GetComponent<FadeToBlack>().DisableBlackScreen();
        GetComponent<PlayerHealthScript>().regainHealth(100);
        transform.position = respawnPoint.transform.position;
        invincibility = false;

    }

    public void SetRespawnPoint(Vector3 position)
    {
        //Debug.Log("Respawnpoint Set");
        respawnPoint.transform.position = new Vector3(position.x, position.y + 1f, position.z);
    }

   

    public float GetFlaskUses() {

        return flaskUses;
    }

    public void ResetPotions()
    {
        if (flaskUses < originalFlaskUsesAmount)
        {
            SetFlaskUses(originalFlaskUsesAmount);
        }
       
    }

    public void ResetPotionsToOriginal()
    {
        SetFlaskUses(originalFlaskUsesAmount);
    }




    public void SetFlaskUses(int x)
    {
        flaskUses = x;
    }

    public bool InputDelayTimer()
    {

        delayTimer += Time.deltaTime;

        if (delayTimer >= inputDelay)
        {

            delayTimer = 0;
            return true;

        }

        return false;
    }

    public void ChangeInputDelayOn() {
        if (inputDelayOn == false)
        {
            inputDelayOn = true;
        }
        else {
            inputDelayOn = false;
        }
    
    }


    // Configs

    public void setConfig(int newMaxMana, float newMoveSpeed)
    {
        maxMana = newMaxMana;
        moveSpeed = newMoveSpeed;
    }

    public int getMaxMana()
    {
        return maxMana;
    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }



    // END Configs

    public GameObject FindClosestEnemy()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in enemies)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public void updateAutoaim()
    {


        INIParser ini = new INIParser();
        ini.Open(Application.persistentDataPath + "ProtoConfig.ini");
        autoAim = ini.ReadValue("Henrik", "autoAim;", true);
        ini.Close();


        //autoAim = x;
    }

    

}


