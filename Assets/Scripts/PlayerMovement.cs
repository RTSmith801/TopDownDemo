﻿using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;    

    Vector2 movement = new Vector2(0, 0);
    Vector2 lookDirection = new Vector2(0, 0);
    public Vector2 lastLookDirection = new Vector2(0, -1);

    // This currently isn't doing anything, but thought it would be good info to have on the player.
    public string lastLookDirectionDefined = "South";
    
    bool isRunning = false;
    public bool canMove = true;

    // // I'd like to set all player stats from a single script. 
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
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove)
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

        // Prevents changing look direction while attacking.
        
        
        animator.SetFloat("Horizontal", lastLookDirection.x);
        animator.SetFloat("Vertical", lastLookDirection.y);
        

        if (!isRunning)
            animator.SetFloat("Speed", (movement.magnitude * moveSpeed) / moveSpeed);
        else if (isRunning)
            animator.SetFloat("Speed", (movement.magnitude * (moveSpeed * runSpeedMultiplier) / moveSpeed));
    }


    // Use this for physics updates.
    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        // Primary purpose is to prevent movement while attacking.
        if (canMove)
        {
            if (isRunning == false)
            {
                rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);                
            }

            else if (isRunning == true)
            {   
                rb.MovePosition(rb.position + movement * (moveSpeed * runSpeedMultiplier) * Time.fixedDeltaTime);                
            }
        }

        // Prevents player from drifting while attacking
        else if (!canMove)
        {
            rb.velocity = Vector2.zero;
        }
    }

    // This method exists on multiple scripts and is called in the animator.
    public void AnimationExit()
    {
        //print("This was called from Player Movement");
        canMove = true;
    }

    public void Death()
    {        
        canMove = false;
    }
}
