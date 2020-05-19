using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    LayerMask hitLayers;
    Transform attackPoint;
    Rigidbody2D rb;
    int attackDamage;
    float attackRange;
    float knockbackStrength;
    float knockbackTime;
    float arrowFlytime;
    //float currentFlytime = 0f;          

    public void ArrowLaunch(LayerMask hitLayersX, Transform attackPointX, int attackDamageX, float attackRangeX, float knockbackStrengthX, float knockbackTimeX, float arrowFlytimeX)
    {
        hitLayers = hitLayersX;
        attackPoint = attackPointX;
        attackDamage = attackDamageX;
        attackRange = attackRangeX;
        knockbackStrength = knockbackStrengthX;
        knockbackTime = knockbackTimeX;
        arrowFlytime = arrowFlytimeX;
        LateStart();
    }

    void LateStart()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * 10;
        Invoke("SelfDestruct", arrowFlytime);
    }   

    private void OnCollisionEnter2D(Collision2D hit)
    {
        if (gameObject.tag == "PlayerWeapon" && hit.gameObject.CompareTag("Player"))
        {
            return;
        }

        GetComponent<Collider2D>().isTrigger = true;
        transform.parent = hit.gameObject.transform;        
        rb.velocity = Vector2.zero;

        if (hit.gameObject.CompareTag("Player") || hit.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D rb = hit.transform.GetComponent<Rigidbody2D>();
            hit.transform.GetComponent<HealthManager>().TakeDamage(attackDamage);
                        
            if (hit.gameObject.CompareTag("Enemy"))
            {
                EnemyBaseClass ebc = rb.GetComponent<EnemyBaseClass>();
                ebc.ChangeState(EnemyState.stagger);                                
                ebc.ChangeState(EnemyState.idle, knockbackTime);
            }            
            Vector2 direction = hit.transform.position - transform.position;            
            rb.AddForce(direction.normalized * knockbackStrength, ForceMode2D.Impulse);
        }
    }

    void SelfDestruct()
    {
        Destroy(this.gameObject);
        //print("Arrow self-destruct");
    }

}
