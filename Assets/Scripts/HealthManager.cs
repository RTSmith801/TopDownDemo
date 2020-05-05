using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    GameManager gm;
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
        gm = FindObjectOfType<GameManager>();
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
            if (gameObject.CompareTag("Player"))
            {   
                GetComponent<PlayerManager>().ChangeState(PlayerState.stagger);             
            }

            isInvincible = true;
            Invoke("Vulnerable", flashWhiteDurration);
            //Interruption();
            healthCurrent -= damageAmount;
            if (healthCurrent <= 0)
            {
                Die();
                return;
            }
            flashWhite.FlashWhiteCalled();
            SoundCue();
        }
    }

    private void SoundCue()
    {
        if (gameObject.CompareTag("Player"))
        {
            if (isAlive)
            {
                int x = Random.Range(1, 3);
                gm.am.Play("PlayerHit" + x);
            }

            else
            {
                gm.am.Play("PlayerDeath");
            }
        }

        else
        {
            if (isAlive)
            {
                int x = Random.Range(1, 4);
                gm.am.Play("SlimeHit" + x);
            }

            else
            {
                gm.am.Play("SlimeDeath");
            }
        }
    }

    //public void Interruption()
    //{
    //    animator.ResetTrigger("Attack");        
    //}

    void Vulnerable()
    {
        isInvincible = false;
    }

    void Die()
    {
        print(gameObject.name + " has died");    
        isAlive = false;
        SoundCue();
        animator.SetBool("isAlive", false);
        // Death effect & animation

        // The following will only occur if death is not on Player.
        if (gameObject.CompareTag("Player"))
        {
            // I only want the sprite flash on the player when he dies.
            flashWhite.FlashWhiteCalled();
            GetComponent<PlayerManager>().Death();
            GetComponent<Rigidbody2D>().isKinematic = true;
            return;
        }

        col.enabled = false;
        Destroy(gameObject, 1.5f);
    }
}
