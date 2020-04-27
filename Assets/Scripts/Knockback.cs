using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    float knockbackStrength = 10f;
    float knockbackTime = .15f;


    private void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            if (other.gameObject.CompareTag("Player"))
                rb.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;

            Vector2 direction = other.transform.position - transform.position;

            rb.AddForce(direction.normalized * knockbackStrength, ForceMode2D.Impulse);
            StartCoroutine(Knocked(rb));
        }
    }

    private IEnumerator Knocked(Rigidbody2D other)
    {
        if (other != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            other.velocity = Vector2.zero;

            if (other.gameObject.CompareTag("Player"))
                other.GetComponent<PlayerMovement>().currentState = PlayerState.idle;
        }        
    }
}

