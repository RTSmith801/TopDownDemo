using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{    
    public int enemyHealth;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    GameObject currentRoom;    
    Rigidbody2D rb;
    CircleCollider2D col;
    Vector2 movement = new Vector2(0, 0);
    public Transform enemyTarget;
    public float chaseRadius;
    public bool enemmyTargetWithinRadius = false;

    private void Start()
    {
        StartingAssignments();
        BaseStats();
    }

    protected virtual void StartingAssignments()
    {
        enemyTarget = GameObject.FindWithTag("Player").transform;
        currentRoom = transform.parent.gameObject;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    } 

    protected virtual void BaseStats()
    {
        // Base stats
        enemyHealth = 3;
        enemyName = "Unidentified Enemy";
        baseAttack = 1;
        moveSpeed = 1f;
        chaseRadius = 4f;
    }

    private void Update()
    {
        CheckDistanceToTarget();        
    }


    void CheckDistanceToTarget()
    {       
        if (Vector2.Distance(enemyTarget.position, transform.position) <= chaseRadius)
        {
            enemmyTargetWithinRadius = true;
            movement = enemyTarget.position - transform.position;
        }
        else
        {
            movement = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        if (!enemmyTargetWithinRadius)
            return;

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerWeapon"))
        {
            print("player has hit enemy");
        }

        else if (other.gameObject.CompareTag("Player"))
        {
            print("enemy has hit player");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }

}
