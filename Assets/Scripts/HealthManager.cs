using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    CircleCollider2D col;
    public Animator animator;
    public FlashWhite flashWhite;
    float flashWhiteDurration;

    [Header("Health Related Stats")]
    public bool isInvincible;
    public bool isAlive;
    public int healthMax = 3;
    public int healthCurrent;


    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        flashWhite = GetComponent<FlashWhite>();
        flashWhiteDurration = flashWhite.flashWhiteDurration;        
    }

    private void OnEnable()
    {
        isAlive = true;
        animator.SetBool("isAlive", true);
        isInvincible = false;
        healthCurrent = healthMax;        
    }
    
    public void TakeDamage(int damageAmount)
    {
        if (!isInvincible && isAlive)
        {
            isInvincible = true;
            Invoke("Vulnerable", flashWhiteDurration);
            flashWhite.FlashWhiteCalled();
            Interruption();
            healthCurrent -= damageAmount;
            if (healthCurrent <= 0)
            {
                Die();
                return;
            }
        }
    }

    public void Interruption()
    {
        animator.ResetTrigger("Attack");        
    }

    void Vulnerable()
    {
        isInvincible = false;
    }

    void Die()
    {
        print(gameObject.name + " has died");    
        isAlive = false;
        animator.SetBool("isAlive", false);
        // Death effect & animation

        // The following will only occur if death is not on Player.
        if (gameObject.CompareTag("Player"))
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            return;
        }

        col.enabled = false;
        Destroy(gameObject, 1.5f);
    }
}
