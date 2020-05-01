using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState { idle, walk, attack, stagger, dead, roomChange }

public class PlayerManager : MonoBehaviour
{
    public PlayerState currentState;

    GameManager gm;
    Rigidbody2D rb;
    Animator animator;
    PlayerMovement pm;

    // Movement Variables
    [HideInInspector] public Vector2 movement = new Vector2(0, 0);
    [HideInInspector] public Vector2 lookDirection = new Vector2(0, 0);
    [HideInInspector] public Vector2 lastLookDirection = new Vector2(0, -1);
    public string lastLookDirectionDefined = "South";
    public bool isRunning = false;
    public float moveSpeed = 5f;
    public float runSpeedMultiplier = 1.5f;

    // XBox Controller Settings
    PlayerControls XBoxControllerInput;
    // A high deadzone allows for optional right joystick use.
    float rightStickDeadZone = .9f;


    // Use Awake(), OnEnable(), and OnDisable() to identify controller input
    private void Awake()
    {

        XBoxControllerInput = new PlayerControls();

        XBoxControllerInput.Gameplay.LeftStick.performed += ctx => movement = ctx.ReadValue<Vector2>();
        XBoxControllerInput.Gameplay.LeftStick.canceled += ctx => movement = Vector2.zero;

        XBoxControllerInput.Gameplay.RightStick.performed += ctx => lookDirection = ctx.ReadValue<Vector2>();
        XBoxControllerInput.Gameplay.RightStick.canceled += ctx => lookDirection = Vector2.zero;

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
        ChangeState(PlayerState.idle);
        gm = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();        
    }

    void Update()
    {
        if (currentState == PlayerState.idle || currentState == PlayerState.walk)
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

    }

    private void LockAxis(Vector2 inputDirection)
    {
        //if (!canMove)
        //    return;

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
            lastLookDirectionDefined = "Undefined last look direction = (" + lastLookDirection.x + ", " + lastLookDirection.y + ")";
        }
    }


    // Use this for physics updates.
    private void FixedUpdate()
    {
        if (currentState == PlayerState.idle || currentState == PlayerState.walk)
        {
            pm.movement = movement;
            pm.lookDirection = lookDirection;
            pm.lastLookDirection = lastLookDirection;
            pm.isRunning = isRunning;
            pm.moveSpeed = moveSpeed;
            pm.runSpeedMultiplier = runSpeedMultiplier;
            pm.PlayerMove();
        }
    }

    // This method exists on multiple scripts and is called in the animator.
    public void AnimationExit()
    {
        //canMove = true;
        if (currentState != PlayerState.dead)
            ChangeState(PlayerState.idle);
    }

    public void Death()
    {
        //canMove = false;
        ChangeState(PlayerState.dead);
    }

    public void ChangeState(PlayerState newState)
    {
        if (currentState != newState)
            currentState = newState;
    }
}
