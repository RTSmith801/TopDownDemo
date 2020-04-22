using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

    public float moveSpeed = 5f;
    Vector2 movement = new Vector2(0, 0);
    Vector2 lookDirection = new Vector2(0, 0);
    Vector2 lastLookDirection = new Vector2(0, -1);
    public bool isAttacking = false;
    
    // XBox Controller Settings
    PlayerControls XBoxControllerInput;    
    // A high deadzone allows for optional right joystick use.
    float rightStickDeadZone = .9f;


    private void Awake()
    {
        XBoxControllerInput = new PlayerControls();

        XBoxControllerInput.Gameplay.LeftStick.performed += ctx => movement = ctx.ReadValue<Vector2>();
        XBoxControllerInput.Gameplay.LeftStick.canceled += ctx => movement = Vector2.zero;
                
        XBoxControllerInput.Gameplay.RightStick.performed += ctx => lookDirection = ctx.ReadValue<Vector2>();
        XBoxControllerInput.Gameplay.RightStick.canceled += ctx => lookDirection = Vector2.zero;

        XBoxControllerInput.Gameplay.X.performed += ctx => PlayerAttack();
        XBoxControllerInput.Gameplay.RightBumper.performed += ctx => PlayerAttack();
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

    // Update is called once per frame
    void Update()
    {
        GetControllerInput();
    }

    private void GetControllerInput()
    {
        // Right Joystick will override look direction so character can face in a user defined direction while moving in a different direction.
        // This will default to matching movement controlls if look direction is not defined by user.
        if ((lookDirection.magnitude <= rightStickDeadZone) && (!Mathf.Approximately(movement.x, 0.0f) || !Mathf.Approximately(movement.y, 0.0f)))
        {
            //lastLookDirection.Set(movement.x, movement.y);
            //lastLookDirection.Normalize();
            LockAxis(movement);
        }

        else if (lookDirection.magnitude > rightStickDeadZone)
        {
            //lastLookDirection = lookDirection;
            LockAxis(lookDirection);
        }
        
    }

    private void LockAxis(Vector2 inputDirection)
    {
        // Attempting to lock look direction to one of four directions.  This should prevent triggering of multiple direction's attack animations.
        if (Mathf.Abs(inputDirection.x) > Mathf.Abs(inputDirection.y))
        {
            lastLookDirection.Set(inputDirection.x, 0);
        }

        else if (Mathf.Abs(inputDirection.x) < Mathf.Abs(inputDirection.y))
        {
            lastLookDirection.Set(0, inputDirection.y);
        }

        else
        {
            // For debugging purposes.
            print("You have encountered a wild problem");
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

        // The purpose of setting the float of "Speed" to (movement.normalized * moveSpeed) is to allow for the character's
        // walking animation to match the a joystick's input, or to allow speeds less than full to be recorded.
        animator.SetFloat("Speed", (movement.magnitude * moveSpeed) / moveSpeed);

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
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
