using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{   
    GameManager gm;
    //LayerMask hitLayers;
    //Transform attackPoint;        
    //int attackDamage = 1;
    //float attackRange = .5f;
    //float knockbackStrength = 10f;
    //float knockbackTime = .1f; 

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void PlayerAttackCalled(LayerMask hitLayers, Transform attackPoint, int attackDamage, float attackRange, float knockbackStrength, float knockbackTime)
    {
        Collider2D[] hitRegistered = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hitLayers);

        foreach(Collider2D hit in hitRegistered)
        {
            //print("hit " + enemy.name);
            hit.GetComponent<HealthManager>().TakeDamage(attackDamage);
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                print("Knockback collision has been called on " + hit.gameObject.name);
                rb.GetComponent<EnemyBaseClass>().currentState = EnemyState.stagger;
                                        
                Vector2 direction = hit.transform.position - transform.position;
                //direction.y = 0;

                rb.AddForce(direction.normalized * knockbackStrength, ForceMode2D.Impulse);
                StartCoroutine(Knocked(rb, knockbackTime));
            }
        }
    }

    private IEnumerator Knocked(Rigidbody2D enemy, float knockbackTime)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            enemy.velocity = Vector2.zero;
            enemy.GetComponent<EnemyBaseClass>().currentState = EnemyState.idle;
        }
    }


    private void SoundCue()
    {
        if (gameObject.CompareTag("Player"))
        {
            int x = Random.Range(1, 6);
            gm.am.Play("Attack" + x);
            
        }

        else
        {
            // Enemy Attack Sound goes here.
        }
    }
}
