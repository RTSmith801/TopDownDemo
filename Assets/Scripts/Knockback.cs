using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    float knockbackStrength = 15f;
    float knockbackTime = .2f;


    private void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
        print("Knockback collision has been called");

        if (rb != null)
        {
            print("Knockback collision has been called on " + other.gameObject.name);

            //other.gameObject.GetComponent<PlayerMovement>().canMove = false;            
            Vector2 direction = other.transform.position - transform.position;
            //direction.y = 0;

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
            //other.isKinematic = true;
        }        
    }
}

