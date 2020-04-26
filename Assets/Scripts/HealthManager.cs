using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public Animator animator;
    public FlashWhite flashWhite;
    float flashWhiteDurration;
    
    [Header("Health Related Stats")]
    public bool isInvincible;
    public bool isAlive;
    public int healthMax = 3;
    public int healthCurrent;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        flashWhite = GetComponent<FlashWhite>();
        flashWhiteDurration = flashWhite.flashWhiteDurration;

        isAlive = true;
        isInvincible = false;
        healthCurrent = healthMax;
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isInvincible)
        {
            isInvincible = true;
            Interruption();
            healthCurrent -= damageAmount;
            flashWhite.FlashWhiteCalled();
            Invoke("Vulnerable", flashWhiteDurration);

            if (healthCurrent <= 0)
            {
                Die();
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
        // Death effect & animation
        Destroy(gameObject, 1.5f);
    }
}
