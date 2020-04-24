using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

    public float moveSpeed = 5f;
    public float runSpeedMultiplier = 1.5f;
    Vector2 movement = new Vector2(0, 0);
    Vector2 lookDirection = new Vector2(0, 0);
    Vector2 lastLookDirection = new Vector2(0, -1);
    public string lastLookDirectionDefined = "South";
    public bool isAttacking = false;
    public bool isRunning = false;
    
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
    }

    
    void Update()
    {
        GetControllerInput();
        LastLookDirectionDefined();
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

        else
        {
            print("You have encountered a controller input issue");
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
            // For debugging purposes.
            print("You have encountered a wild problem");
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
        print("Attack has been called with look direction = " + lastLookDirection.x + ", " + lastLookDirection.y);
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
}
