using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    [HideInInspector] public Vector2 movement = new Vector2(0, 0);
    [HideInInspector] public Vector2 lookDirection = new Vector2(0, 0);
    [HideInInspector] public Vector2 lastLookDirection = new Vector2(0, -1);       
    [HideInInspector] public bool isRunning;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public float runSpeedMultiplier;
    
    private void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }    
   
    // Call this from FixedUpdate() on PlayerManager.cs.
    public void PlayerMove()
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

    //private IEnumerator AutoMovePlayer(Rigidbody2D rb, float moveSpeed)
    //{
    //    if (rb != null)
    //    {
    //        float currentTime = 0f;
    //        while (currentTime < (gm.fadeSpeed))
    //        {
    //            currentTime += Time.deltaTime;
    //            print("MovePlayer from Room Property being called with direction = " + direction);
    //            rb.MovePosition(rb.position + -direction * moveSpeed * Time.fixedDeltaTime);
    //            yield return null;
    //        }
    //        rb.velocity = Vector2.zero;
    //        rb.GetComponent<PlayerMovement>().currentState = PlayerState.idle;
    //        StopCoroutine(MovePlayer(rb, moveSpeed));
    //    }
    //}
}
