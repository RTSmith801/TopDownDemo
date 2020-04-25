using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement = new Vector2(0, 0);
    Vector2 lookDirection = new Vector2(0, 0);
    Vector2 lastLookDirection = new Vector2(0, -1);
    public string lastLookDirectionDefined = "South";
    public bool isAttacking = false;
    public bool isRunning = false;
    public bool isAlive = true;

    [Header("Player Stats")]
    public bool isInvincible = false;
    public int playerHealthMax = 3;
    public int playerHealthCurrent;
    public int playerAttackDamage = 1;
    public float moveSpeed = 5f;
    public float runSpeedMultiplier = 1.5f;

    public SpriteRenderer sr;
    public Material matDefault;
    public Material matFlashWhite;
    public Material matDestination;
    public float flashWhiteSpeed = 0.5f;
    public float flashWhiteDurration = 3f;
    public bool takingDamage = false;
    public float flashWhiteDurrationTimer = 0;
    public float flashWhiteSpeedTimer = 3;

    // XBox Controller Settings
    PlayerControls XBoxControllerInput;    
    // A high deadzone allows for optional right joystick use.
    float rightStickDeadZone = .5f;


    private void Awake()
    {
        // List all input options here.

        XBoxControllerInput = new PlayerControls();

        XBoxControllerInput.Gameplay.LeftStick.performed += ctx => movement = ctx.ReadValue<Vector2>();
        XBoxControllerInput.Gameplay.LeftStick.canceled += ctx => movement = Vector2.zero;
                
        XBoxControllerInput.Gameplay.RightStick.performed += ctx => lookDirection = ctx.ReadValue<Vector2>();
        XBoxControllerInput.Gameplay.RightStick.canceled += ctx => lookDirection = Vector2.zero;

        XBoxControllerInput.Gameplay.X.performed += ctx => PlayerAttack();
        XBoxControllerInput.Gameplay.RightBumper.performed += ctx => PlayerAttack();

        XBoxControllerInput.Gameplay.B.performed += ctx => isRunning = true;
        XBoxControllerInput.Gameplay.B.canceled += ctx => isRunning = false;

        XBoxControllerInput.Gameplay.LeftBumper.performed += ctx => isRunning = true;
        XBoxControllerInput.Gameplay.LeftBumper.canceled += ctx => isRunning = false;
    }

    private void OnEnable()
    {
        XBoxControllerInput.Gameplay.Enable();
    }

    private void OnDisable()
    {
        XBoxControllerInput.Gameplay.Disable();
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isAlive = true;
        playerHealthCurrent = playerHealthMax;
        sr = GetComponent<SpriteRenderer>();
        matDefault = sr.material;
        matDestination = matDefault;
        matFlashWhite = Resources.Load("FlashWhite", typeof(Material)) as Material;
        //Invoke ("Test", 2f);
    }

    private void Test()
    {
        sr.material = null;
        sr.material = matFlashWhite;
        print("sr.material = " + sr.material);
        Invoke("Test2", 2f);
    }

    private void Test2()
    {
        sr.material = null;
        sr.material = matDefault;
        print("sr.material = " + sr.material);
        Invoke("Test", 2f);
    }

    void Update()
    {
        GetControllerInput();
        LastLookDirectionDefined();

        if (takingDamage == true)
        {
            // Timers 
            //flashWhiteDurrationTimer += Time.deltaTime;
            //flashWhiteSpeedTimer += Time.deltaTime;
            FlashWhite();
        }
    }

    private void GetControllerInput()
    {
        // Right Joystick will override look direction so character can face in a user defined direction while moving in a different direction.
        // This will default to matching movement controlls if look direction is not defined by user.
        if ((lookDirection.magnitude <= rightStickDeadZone) && (!Mathf.Approximately(movement.x, 0.0f) || !Mathf.Approximately(movement.y, 0.0f)))
        {   
            LockAxis(movement);
        }

        else if (lookDirection.magnitude > rightStickDeadZone)
        {         
            LockAxis(lookDirection);
        }      
        
    }

    private void LockAxis(Vector2 inputDirection)
    {
        // Attempting to lock look direction to one of four directions.  This should prevent triggering of multiple direction's attack animations.
        if (Mathf.Abs(inputDirection.x) > Mathf.Abs(inputDirection.y))
        {
            lastLookDirection.Set(inputDirection.x, 0);
            lastLookDirection.Normalize();
        }

        else if (Mathf.Abs(inputDirection.x) < Mathf.Abs(inputDirection.y))
        {
            lastLookDirection.Set(0, inputDirection.y);
            lastLookDirection.Normalize();
        }

        else
        {
            lastLookDirection.Normalize();
        }
    }

    void LastLookDirectionDefined()
    {
        if (lastLookDirection == new Vector2(0, 1))
        {
            lastLookDirectionDefined = "North";            
        }

        else if (lastLookDirection == new Vector2(0, -1))
        {
            lastLookDirectionDefined = "South";
        }

        else if (lastLookDirection == new Vector2(1, 0))
        {
            lastLookDirectionDefined = "East";
        }

        else if (lastLookDirection == new Vector2(-1, 0))
        {
            lastLookDirectionDefined = "West";
        }

        else
        {
            lastLookDirectionDefined = "Undefined = (" + lastLookDirection.x + ", " + lastLookDirection.y + ")";
        }
    }

    private void FixedUpdate()
    {
        // Prevents movement while attacking.
        if (!isAttacking)
        {
            PlayerMove();
        }
        else if (isAttacking)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void PlayerMove()
    {
        animator.SetFloat("Horizontal", lastLookDirection.x);
        animator.SetFloat("Vertical", lastLookDirection.y);

        if (isRunning == false)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            animator.SetFloat("Speed", (movement.magnitude * moveSpeed) / moveSpeed);
        }

        else if (isRunning == true)
        {   
            rb.MovePosition(rb.position + movement * (moveSpeed * runSpeedMultiplier) * Time.fixedDeltaTime);
            animator.SetFloat("Speed", (movement.magnitude * (moveSpeed * runSpeedMultiplier) / moveSpeed));
        }
    }

    private void PlayerAttack()
    {        
        if (isAttacking == true)
        {
            return;
        }
        animator.SetTrigger("Attack");
        isAttacking = true;
    }

    public void PlayerInterruption()
    {
        animator.ResetTrigger("Attack");
        isAttacking = false;
    }

    public void PlayerTakeDamage(int damageAmount)
    {
        if (!isInvincible)
        {
            isInvincible = true;
            takingDamage = true;
            print("Start = sr.material = " + sr.material);
            flashWhiteDurrationTimer = 0;
            playerHealthCurrent -= damageAmount;
            //StartCoroutine(FlashWhite(matDefault, matFlashWhite, flashWhiteSpeed, flashWhiteDurration));
        }
    }

    //IEnumerator FlashWhite(Material matDefault, Material matFlashWhite, float flashWhiteSpeed, float flashWhiteDurration)
    //{
    //    float flashWhiteDurrationTimer = 0f;
    //    float flashWhiteSpeedTimer = flashWhiteSpeed;
    //    while (flashWhiteDurrationTimer <= flashWhiteDurration)
    //    {
    //        flashWhiteDurrationTimer += Time.deltaTime;
    //        flashWhiteSpeedTimer += Time.deltaTime;
    //        if (flashWhiteSpeedTimer >= flashWhiteSpeed)
    //        {                
    //            if (sr.material == matDefault)
    //                sr.material = matFlashWhite;
    //            else
    //                sr.material = matDefault;
    //            flashWhiteSpeedTimer = 0f;
    //            print("sr.material = " + sr.material);
    //        }
    //        yield return null;
    //    }
    //    sr.material = matDefault;
    //    isInvincible = false;
    //    StopCoroutine(FlashWhite(matDefault, matFlashWhite, flashWhiteSpeed, flashWhiteDurration));
    //}

    private void LateUpdate()
    {
        //animator.SetFloat("Horizontal", lastLookDirection.x);
        //animator.SetFloat("Vertical", lastLookDirection.y);
        //animator.SetFloat("Speed", (movement.magnitude * moveSpeed) / moveSpeed);
        sr.material = matDestination;

        //// Created a coroutine in LateUpate to override animations created in Mechanim
        //if (takingDamage == true)
        //{
        //    FlashWhite();
        //}
    }  
    
    void FlashWhite()
    {
        flashWhiteDurrationTimer += Time.deltaTime;

        if (flashWhiteDurrationTimer <= flashWhiteDurration)
        {
            flashWhiteSpeedTimer += Time.deltaTime;

            if (flashWhiteSpeedTimer >= flashWhiteSpeed)
            {
                if (matDestination == matDefault)
                {
                    matDestination = matFlashWhite;
                }

                else
                {
                    matDestination = matDefault;
                }

                print("sr.material = " + sr.material);
                flashWhiteSpeedTimer = 0f;
                return;
            }
        }

        else if (flashWhiteDurrationTimer > flashWhiteDurration)
        {
            takingDamage = false;
            isInvincible = false;
            matDestination = matDefault;
            flashWhiteDurrationTimer = 0f;
            flashWhiteSpeedTimer = flashWhiteSpeed;
            print("End = sr.material = " + sr.material);
        }               

    }
}
