using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    float knockbackStrength = 10f;
    float knockbackTime = .15f;


    private void OnCollisionEnter2D(Collision2D other)
    {        
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {   
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    PlayerManager pm = rb.GetComponent<PlayerManager>();                    
                    pm.ChangeState(PlayerState.stagger);
                    pm.ChangeState(PlayerState.idle, knockbackTime);
                }   

                Vector2 direction = other.transform.position - transform.position;
                rb.AddForce(direction.normalized * knockbackStrength, ForceMode2D.Impulse);                
            }
        }
    }
}

