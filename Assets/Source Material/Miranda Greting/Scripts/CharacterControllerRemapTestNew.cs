using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Main Authour : Tim Agélii
public class CharacterControllerRemapTestNew : MonoBehaviour
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

    //rebinding
    PlayerInputs inputActions;
    //move rebindings
    Vector2 movementInput;
    private bool dodgerollActivated;
    private bool attackActivated = false;
    private bool attackDone = false;
    private bool attackCanceled = false;
    private bool keyboardUsed;
    private string controlUsed;


    private void OnEnable()
    {
        //inputActions.WindlessLand.Dodgeroll.started += Dodgeroll;
        //inputActions.WindlessLand.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.WindlessLand.Move.performed += MovePerformed;
        inputActions.WindlessLand.Attack.started += ctx => attackActivated = true;
        inputActions.WindlessLand.Attack.performed += ctx => attackDone = true;
        inputActions.WindlessLand.Attack.canceled += ctx => attackCanceled = true;
        inputActions.WindlessLand.Dodgeroll.performed += ctx => dodgerollActivated = true;
        inputActions.WindlessLand.Enable();
    }

    private void MovePerformed(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
        controlUsed = ctx.control.path;
    }

    private void AttackStarted()
    {

    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Awake()
    {
        inputActions = InputManager.inputActions;
    }
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

        currentAttackTrigger = "Attack1";
        //  gameObject.GetComponent<ArrowUI>().UpdateAmmo(mana, maxMana);
        inputActions = InputManager.inputActions;
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

            EquipManager();

            BowManager();




            //anim stuff here. 
            anim.SetFloat("XSpeed", movementInput.x);
            //anim.SetFloat("XSpeed", Input.GetAxis("HorizontalKey"));
            anim.SetFloat("YSpeed", movementInput.y);
            //anim.SetFloat("YSpeed", Input.GetAxis("VerticalKey"));
        }
    }

    private void FixedUpdate()
    {


    }

    private void PlayerRotationUpdate()
    {
        if (moveAllow && (Mathf.Abs(movementInput.x) + Mathf.Abs(movementInput.y)) != 0)
        {
            //Debug.Log(controlUsed);
            Vector3 horizontal = movementInput.x * right;
            Vector3 vertical = movementInput.y * forward;
            if ((inputActions.WindlessLand.Move.bindings[3].effectivePath.Equals("<Keyboard>/a") || inputActions.WindlessLand.Move.bindings[3].effectivePath.Equals("<Keyboard>/leftArrow")) && (inputActions.WindlessLand.Move.bindings[4].effectivePath.Equals("<Keyboard>/d") || inputActions.WindlessLand.Move.bindings[4].effectivePath.Equals("<Keyboard>/rightArrow")) && !controlUsed.Contains("Gamepad")) {
                horizontal = (Input.GetAxis("Horizontal") * right);
            }

            if ((inputActions.WindlessLand.Move.bindings[1].effectivePath.Equals("<Keyboard>/w") || inputActions.WindlessLand.Move.bindings[1].effectivePath.Equals("<Keyboard>/upArrow")) && (inputActions.WindlessLand.Move.bindings[2].effectivePath.Equals("<Keyboard>/s") || inputActions.WindlessLand.Move.bindings[2].effectivePath.Equals("<Keyboard>/downArrow")) && !controlUsed.Contains("Gamepad"))
            {
                vertical = (Input.GetAxis("Vertical") * forward);
            }
            //Vector3 vertical = movementInput.y * forward; 
            Vector3 rotation = horizontal + vertical;

            //Debug.Log("Inputsystem: " + movementInput.x * right + ",  GetAxis: " + Input.GetAxis("Horizontal") * right  + ", Current: " + horizontal.x);

            transform.rotation = Quaternion.LookRotation(rotation);
        }
        /*
        if (moveAllow && Mathf.Abs(Input.GetAxis("HorizontalKey")) + Mathf.Abs(Input.GetAxis("VerticalKey")) != 0)
        {
            Vector3 horizontal = (Input.GetAxis("Horizontal") * right);
            Vector3 vertical = (Input.GetAxis("Vertical") * forward);
            Vector3 rotation = horizontal + vertical;

            transform.rotation = Quaternion.LookRotation(rotation);
        }
        */





        /*
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("Key Down");
            CanMove();

        }
        */
    }

    void UpdateMoveInput()
    {
        Vector3 rightMovement = right * moveSpeed * Input.GetAxis("HorizontalKey");
        Vector3 upMovement = forward * moveSpeed * Input.GetAxis("VerticalKey");

        Vector3 horizontalMovement = right * moveSpeed * movementInput.x;
        Vector3 verticalMovement = forward * moveSpeed * movementInput.y;
        //movementInput.x = Input.GetAxis("Horizontal");
        //movementInput.y = Input.GetAxis("Vertical");
        //Debug.Log("speed x:" + movementInput.x + "speed y:" + movementInput.y);
        //Debug.Log("Inputspeed x:" + movementInput.x + " AxisSpeed: " + Input.GetAxis("HorizontalKey") + " GeneralAxisSpeed: " + Input.GetAxis("Horizontal"));
        playerMovement = horizontalMovement + verticalMovement;

        //playerMovement = rightMovement + upMovement;

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



    void Move()
    {

        playerRgb.velocity = playerMovement + new Vector3(0, playerRgb.velocity.y, 0);


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

    public int GetMana()
    {
        return mana;
    }


    void EquipManager()
    {
        if (bowIsActive == false && startAttackDelay == false && startAttackCooldown == false)
        {

            if (/*Input.GetKeyDown(KeyCode.Alpha2)*/ inputActions.WindlessLand.EquipBow.triggered) //detects when Weapon2 rebinding is triggered
            {
                mana = 100; //testing - remove later
                bow.SetActive(true);
                sword.SetActive(false);
                Debug.Log("Bow equipped");
            }
            if (/*Input.GetKeyDown(KeyCode.Alpha1)*/ inputActions.WindlessLand.EquipSword.triggered) // detects when weapon1 rebinding trigger
            {
                bow.SetActive(false);
                sword.SetActive(true);
                Debug.Log("Sword equipped");
            }

        }

    }

    void BowManager()
    {
        string keyboardPath = inputActions.WindlessLand.Attack.bindings[0].effectivePath;
        string mousePath = inputActions.WindlessLand.Attack.bindings[1].effectivePath;
        int pathNameIndex = keyboardPath.IndexOf('/')+1;
        int pathNameIndexMouse = mousePath.IndexOf('/')+1;
        if (mousePath.Equals("<Mouse>/leftButton") || mousePath.Equals("<Mouse>/middleButton") || mousePath.Equals("<Mouse>/rightButton")) {
            if(mousePath.Equals("<Mouse>/leftButton"))
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
        //Debug.Log(mousePath + ", " + keyboardPath);
        if (bow.activeSelf == true && dodgerollTimerRunning == false && healthFlaskStart == false)
        {
            if (bowIsActive == false && mana >= bowManaCost)
            {
                if (/*Input.GetKeyDown(KeyCode.Mouse0) attackActivated*/ Input.GetKeyDown(keyboardPath)) //detects when key (re-)binding is pressed
                {
                    StartBowDraw();
                    Debug.Log("started!!");
                    keyboardUsed = true;
                }
                if (/*Input.GetKeyDown(KeyCode.Mouse0) attackActivated*/ Input.GetKeyDown(mousePath)) //detects when key (re-)binding is pressed
                {
                    StartBowDraw();
                    Debug.Log("started!!");
                    keyboardUsed = false;
                }

            }

            if (bowIsActive == true)
            {
                if (drawBow)
                {
                    transform.rotation = lookRotation;
                    DrawBow(keyboardPath, mousePath);
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

                if (/*Input.GetKeyUp(KeyCode.Mouse0) inputActions.WindlessLand.Attack.triggered attackCanceled  (Input.GetKeyUp(keyboardPath) || Input.GetKeyUp(mousePath))*/ ((!Input.GetKey(keyboardPath) && keyboardUsed) || (!Input.GetKey(mousePath) && !keyboardUsed)) && bowIsFinishedLoading == true)
                {
                    BowFire();
                    attackActivated = false;
                    attackCanceled = false;
                    Debug.Log("Fire");
                }

                if (startBowCooldown)
                {
                    BowCooldown();

                }

                if ((/*Input.GetKeyUp(KeyCode.Mouse0) attackCanceled*/ ((!Input.GetKey(keyboardPath) && keyboardUsed) || (!Input.GetKey(mousePath) && !keyboardUsed))   && drawBow == false && bowIsFinishedLoading == false && startBowCooldown == false && drawBow == false) || drawBow == false && queueBowCancel == true)
                {
                    Debug.Log("Cancelbow");
                    attackActivated = false;
                    BowCancel();
                    attackCanceled = false;
                }
            }
        }




    }
    void StartBowDraw()
    {

        bowIsActive = true;
        anim.SetTrigger("DrawBow");

        moveAllow = false;

        drawBow = true;

    }
    void DrawBow(string keyboardPath, string mousePath)
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
            if (/*Input.GetKeyUp(KeyCode.Mouse0) inputActions.WindlessLand.Attack.triggered*/ ((!Input.GetKey(keyboardPath) && keyboardUsed) || (!Input.GetKey(mousePath) && !keyboardUsed)))
            {
                queueBowCancel = true;
            }

        }

    }

    void BowLoading()
    {


        if (AttackWaitTimer(bowChargeTime))
        {
            Debug.Log("bowIsCharged");
            bowIsFinishedLoading = true;
            bowIsLoading = false;
        }
        else
        {

        }
    }

    void BowFire()
    {
        InstantiateArrow();
        mana = mana - bowManaCost;
        Debug.Log(mana + "  manaleft");
        //   gameObject.GetComponent<ArrowUI>().UpdateAmmo(mana, maxMana);
        bowIsFinishedLoading = false;
        anim.SetTrigger("StopBow");
        anim.SetTrigger("BowRecoil");

        startBowCooldown = true;

    }
    void BowCancel()
    {
        anim.SetTrigger("StopBow");
        moveAllow = true;
        queueBowCancel = false;
        attackTimer = 0;
        drawBow = false;
        bowIsLoading = false;
        bowIsFinishedLoading = false;
        startBowCooldown = false;

        bowIsActive = false;

    }



    void BowCooldown()
    {
        if (AttackWaitTimer(bowCooldownTime))
        {
            moveAllow = true;
            startBowCooldown = false;
            bowIsActive = false;
            anim.SetTrigger("StopBow");

        }
        else
        {

        }
    }



    void HealthFlaskManager()
    {
        if (/*Input.GetKeyDown(KeyCode.Q)*/ inputActions.WindlessLand.HealthRefill.triggered && healthFlaskOfCooldown && flaskUses > 0 && startAttackDelay == false && startAttackCooldown == false && dodgerolling == false && bowIsActive == false && gameObject.GetComponent<PlayerHealthScript>().GetHealth() < gameObject.GetComponent<PlayerHealthScript>().GetMaxHealth())
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

                    GetComponentInParent<PlayerHealthScript>().regainHealth(1);

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

    void StartDodgeroll()
    {

        dodgerollStart = true;
        dodgerollTimerRunning = true;
        dodgerollActivated = false; //new

        //more anim things
        //Debug.Log("in player Dodgeroll");
        anim.SetTrigger("DodgeRoll");



        moveAllow = false;

        if (playerInputActive)
        {
            transform.rotation = Quaternion.LookRotation(inputDirection);
        }



        dodgerolling = true;
        dodgerollOfCooldown = false;
        invincibility = true;
    }

    void DodgerollManager()
    { // new
        if (/*Input.GetKeyDown(KeyCode.Space)*/ dodgerollActivated && dodgerollOfCooldown && startAttackDelay == false && startAttackCooldown == false && bowIsActive == false)
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
                        playerRgb.velocity = ((transform.forward).normalized * dodgerollSpeed) + new Vector3(0, playerRgb.velocity.y, 0); ;
                    }
                    else
                    {
                        invincibility = false;
                        playerRgb.velocity = ((transform.forward).normalized * dodgerollDropSpeed) + new Vector3(0, playerRgb.velocity.y, 0);
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

    void AttackManager()
    {
        if (sword.activeSelf == true && startAttackDelay == false && startAttackCooldown == false && dodgerollTimerRunning == false && healthFlaskStart == false)
        {
            if (/*Input.GetKeyDown(KeyCode.Mouse0)*/ inputActions.WindlessLand.Attack.triggered)
            {
                ResetAttackAni();

                Attack();
                Debug.Log("Ataaaaaaackkk (normal)");
            }
            if (/*Input.GetKeyDown(KeyCode.Mouse1)*/ inputActions.WindlessLand.HeavyAttack.triggered && mana >= heavyManaCost)
            {
                mana = mana - heavyManaCost;

                ResetAttackAni();

                HeavyAttack();
                Debug.Log("Heavy attack");
            }

        }

        HitboxDelay();

        AttackCoolDown();
    }

    void Attack()
    {
        attackHitbox = lightAttackHitbox;

        attackDelay = lightAttackDelay;

        swingCooldown = lightSwingCooldown;

        timeToNextSwing = timeToNextSwingLight;

        //more anim things
        //Debug.Log("in player attack");
        anim.SetTrigger(currentAttackTrigger);

        moveAllow = false;
        transform.rotation = lookRotation;
        startAttackDelay = true;

    }

    void HeavyAttack()
    {

        attackHitbox = heavyAttackHitbox;

        attackDelay = heavyAttackDelay;

        swingCooldown = heavySwingCooldown;

        timeToNextSwing = timeToNextSwingHeavy;


        anim.SetTrigger("HeavyAttack");

        moveAllow = false;
        transform.rotation = lookRotation;
        startAttackDelay = true;

    }

    void QueueNextAttackAni()
    {
        currentAttack++;
        if (currentAttack <= attackComboLenght)
        {
            currentAttackTrigger = "Attack" + currentAttack;
        }



    }
    void ResetAttackAni()
    {
        currentAttack = 1;
        currentAttackTrigger = "Attack" + currentAttack;

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

                    queueAttack = true;
                    queueDodge = false;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {

                    queueAttack = false;
                    queueDodge = true;
                }

                if (attackTimer >= timeToNextSwing)
                {

                    if (queueAttack == true && attackDelay == lightAttackDelay)
                    {

                        QueueNextAttackAni();
                        if (currentAttack <= attackComboLenght)
                        {
                            startAttackCooldown = false;
                            queueAttack = false;
                            attackTimer = 0;
                            anim.SetTrigger("StopAttack");

                            Attack();
                        }

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

                playerRgb.velocity = ((transform.forward).normalized * 2f) + new Vector3(0, playerRgb.velocity.y, 0); ;
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

    void InstantiateArrow()
    {
        GameObject arrowPrefab = Instantiate(arrow, transform.position + (transform.rotation * new Vector3(0, 1.5f, 1.5f)), transform.rotation);
    }


    void InstantiateAttackHitbox()
    {
        var newHitbox = Instantiate(attackHitbox, transform.position + (transform.rotation * new Vector3(0, 0.5f, 1.7f)), transform.rotation);

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
        GetComponent<PlayerHealthScript>().regainHealth(100);
        GetComponent<PlayerHealthScript>().ResetPotions();
        Dead = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Player/Dead");
        Dead.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        Dead.start();
        Dead.release();
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        transform.position = respawnPoint.transform.position;
    }

    public void SetRespawnPoint(Vector3 position)
    {
        Debug.Log("Respawnpoint Set");
        respawnPoint.transform.position = new Vector3(position.x, position.y + 2f, position.z);
    }



    public float GetFlaskUses()
    {

        return flaskUses;
    }



    public void SetFlaskUses(int x)
    {
        flaskUses = x;
    }

}


/* newer script bow code fail
 * //long annoying code for checking & converting binding.effectivePath 
        //to a format that works with Input.GetKeyDown etc etc
        //i'm sure there are better ways to do this but it was the first one that worked after lots of testing & googling
        //string keyboardPath = InputManager.GetBindingPath(inputActions.WindlessLand.Attack, 0);
        //string mousePath = InputManager.GetBindingPath(inputActions.WindlessLand.Attack, 1);
        int bindingIndex = 0;
        if (controlUsed.Contains("Gamepad"))
        {
            bindingIndex = 2;
        }
        else if (controlUsed.Contains("Mouse"))
        {
            bindingIndex = 1;
        }
        else if (controlUsed.Contains("Keyboard"))
        {
            bindingIndex = 0;
        }
        Debug.Log(controlUsed);
        Debug.Log(bindingIndex);
        string path = InputManager.GetBindingPath(inputActions.WindlessLand.Attack, bindingIndex);

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
                if (Input.GetKeyDown(KeyCode.Mouse0))//keyboardpath))
                {
                    Debug.Log(path);
                    StartBowDraw();
                    keyboardUsed = true;
                    mouseUsed = false;
                    gamepadUsed = false;
                }
                /*
                if (Input.GetKeyDown(mousePath))
                {
                    StartBowDraw();
                    mouseUsed = true;
                    keyboardUsed = false;
                    gamepadUsed = false;
                }
                else if(Input.GetKeyDown(InputManager.GetBindingPath(inputActions.WindlessLand.Attack, 2)))
                {
                    StartBowDraw();
                    gamepadUsed = true;
                    keyboardUsed = false;
                    mouseUsed = false;
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
        DrawBow(path);
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

    if (/*((!Input.GetKey(keyboardPath) && keyboardUsed) || (!Input.GetKey(mousePath) && mouseUsed) || (!Input.GetKey(InputManager.GetBindingPath(inputActions.WindlessLand.Attack, 2)) && gamepadUsedInput.GetKeyUp(KeyCode.Mouse0) && bowIsFinishedLoading == true)
    {

        BowFire();
    }

    if (startBowCooldown)
    {
        BowCooldown();

    }

    if ((/*((!Input.GetKey(keyboardPath) && keyboardUsed) || (!Input.GetKey(mousePath) && mouseUsed) || (!Input.GetKey(InputManager.GetBindingPath(inputActions.WindlessLand.Attack, 2)) && gamepadUsed)Input.GetKeyUp(KeyCode.Mouse0) && drawBow == false && bowIsFinishedLoading == false && startBowCooldown == false && drawBow == false) || drawBow == false && queueBowCancel == true)
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
private void DrawBow(string rebindPath)
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
        if (Input.GetKeyUp(rebindPath/*KeyCode.Mouse0))
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



private void BowCooldown()
{
    if (AttackWaitTimer(bowCooldownTime))
    {
        transform.GetComponentInParent<PlayerAnimEvents>().SetAllowMovementTrue();
        startBowCooldown = false;
        bowIsActive = false;
        anim.SetTrigger("StopBow");

    }
    else
    {

    }
}
*/