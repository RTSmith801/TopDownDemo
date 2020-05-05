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
    PlayerAttack pa;
        
    [Header ("Movement Variables")]
    [HideInInspector] public Vector2 leftStick = new Vector2(0, 0);
    [HideInInspector] public Vector2 rightStick = new Vector2(0, 0);
    [HideInInspector] public Vector2 lastLookDirection = new Vector2(0, -1);
    public string lastLookDirectionDefined = "South";
    public bool isRunning = false;
    public float moveSpeed = 5f;
    public float runSpeedMultiplier = 1.5f;

    [Header ("Attack Variables")]
    public LayerMask hitLayers;
    public Transform attackPoint;
    public Transform attackPointOffset;
    public float attackRate = .2f;
    public float nextAttackTime = 0f;
    public int attackDamage = 1;
    public float attackRange = 1f;
    public float knockbackStrength = 10f;
    public float knockbackTime = .1f;

    // XBox Controller Settings
    PlayerControls XBoxControllerInput;
    // A high deadzone allows for optional right joystick use.
    float rightStickDeadZone = .9f;


    // Use Awake(), OnEnable(), and OnDisable() to identify controller input
    private void Awake()
    {

        XBoxControllerInput = new PlayerControls();

        XBoxControllerInput.Gameplay.LeftStick.performed += ctx => leftStick = ctx.ReadValue<Vector2>();
        XBoxControllerInput.Gameplay.LeftStick.canceled += ctx => leftStick = Vector2.zero;

        XBoxControllerInput.Gameplay.RightStick.performed += ctx => rightStick = ctx.ReadValue<Vector2>();
        XBoxControllerInput.Gameplay.RightStick.canceled += ctx => rightStick = Vector2.zero;

        XBoxControllerInput.Gameplay.B.performed += ctx => isRunning = true;
        XBoxControllerInput.Gameplay.B.canceled += ctx => isRunning = false;

        XBoxControllerInput.Gameplay.LeftBumper.performed += ctx => isRunning = true;
        XBoxControllerInput.Gameplay.LeftBumper.canceled += ctx => isRunning = false;

        XBoxControllerInput.Gameplay.X.performed += ctx => PlayerAttackCalled();
        XBoxControllerInput.Gameplay.RightBumper.performed += ctx => PlayerAttackCalled();
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
        pa = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        GetControllerInput();        
    }

    private void GetControllerInput()
    {
        // Right Joystick will override look direction so character can face in a user defined direction while moving in a different direction.
        // This will default to matching movement controlls if look direction is not defined by user.
        if ((rightStick.magnitude <= rightStickDeadZone) && (!Mathf.Approximately(leftStick.x, 0.0f) || !Mathf.Approximately(leftStick.y, 0.0f)))
        {
            LockAxis(leftStick);
        }

        else if (rightStick.magnitude > rightStickDeadZone)
        {
            LockAxis(rightStick);
        }

    }

    private void LockAxis(Vector2 inputDirection)
    {
        // Locks look direction to one of four directions.  This should prevent triggering of multiple direction's attack animations.
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
            print("Joysticks are reading the same? " + lastLookDirection);
            lastLookDirection.Normalize();
        }
    }

    // Use this for physics updates.
    private void FixedUpdate()
    {
        // Player movement called here.
        if (currentState == PlayerState.idle || currentState == PlayerState.walk)
        {
            pm.movement = leftStick;
            pm.lookDirection = rightStick;
            pm.lastLookDirection = lastLookDirection;
            pm.isRunning = isRunning;
            pm.moveSpeed = moveSpeed;
            pm.runSpeedMultiplier = runSpeedMultiplier;
            pm.PlayerMove();

            // Rotates attack point
            Rotate();
        }
    }

    private void Rotate()
    {
        Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, lastLookDirection);
        desiredRotation = Quaternion.Euler(0, 0, desiredRotation.eulerAngles.z + 180);
        attackPointOffset.rotation = Quaternion.RotateTowards(attackPointOffset.rotation, desiredRotation, 300);
    }

    public void PlayerAttackCalled()
    {
        if (Time.time >= nextAttackTime && (currentState == PlayerState.idle || currentState == PlayerState.walk))
        {
            ChangeState(PlayerState.attack);
            animator.SetTrigger("Attack");
            pa.PlayerAttackCalled(hitLayers, attackPoint, attackDamage, attackRange, knockbackStrength, knockbackTime);            
            nextAttackTime = Time.time + attackRate;
        }
    }

    // This method can exist on multiple scripts and is called from the player's animation after attack.
    public void AnimationExit()
    {
        if (currentState != PlayerState.dead)
            ChangeState(PlayerState.idle);
    }

    public void Death()
    {   
        ChangeState(PlayerState.dead);
    }

    public void ChangeState(PlayerState newState)
    {
        if (currentState != newState)
            currentState = newState;
    }

    private void OnDrawGizmosSelected()
    {
        // Displays attackPoint.
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
