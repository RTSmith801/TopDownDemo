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
    GameObject arrowPrefab;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        arrowPrefab = Resources.Load("ArrowPrefab", typeof(GameObject)) as GameObject;
    }

    public void PlayerMeeleAttackCalled(LayerMask hitLayers, Transform attackPoint, int attackDamage, float attackRange, float knockbackStrength, float knockbackTime)
    {
        Collider2D[] hitRegistered = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hitLayers);

        foreach(Collider2D hit in hitRegistered)
        {
            //print("hit " + enemy.name);
            hit.GetComponent<HealthManager>().TakeDamage(attackDamage);
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                EnemyBaseClass ebc = rb.GetComponent<EnemyBaseClass>();
                ebc.ChangeState(EnemyState.stagger);                                        
                Vector2 direction = hit.transform.position - transform.position;                
                rb.AddForce(direction.normalized * knockbackStrength, ForceMode2D.Impulse);
                ebc.ChangeState(EnemyState.idle, knockbackTime);
            }
        }
    }

    public void PlayerRangedAttackCalled(LayerMask hitLayers, Transform attackPoint, int attackDamage, float attackRange, float knockbackStrength, float knockbackTime, float arrowFlytime)
    {     
        GameObject newArrow = Instantiate(arrowPrefab, attackPoint);
        Arrow arrow = newArrow.GetComponent<Arrow>();
        arrow.transform.parent = null;
        arrow.tag = "PlayerWeapon";
        arrow.ArrowLaunch(hitLayers, attackPoint, attackDamage, attackRange, knockbackStrength, knockbackTime, arrowFlytime);
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
