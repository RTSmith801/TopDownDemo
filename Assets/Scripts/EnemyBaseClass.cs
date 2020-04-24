using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{    
    public int enemyHealth;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public float chaseRadius;
    public float attackRadius;
    public float attackSpeed;

    GameObject currentRoom;    
    Rigidbody2D rb;
    CircleCollider2D col;
    Vector2 movement = new Vector2(0, 0);
    public Vector2 spawnPos = new Vector2();
    public Transform enemyTarget;
    public bool enemmyTargetWithinRadius = false;
    public bool isAlive = true;

    private void Awake()
    {
        StartingAssignments();
        BaseStats();
    }

    private void OnEnable()
    {
        if (isAlive && spawnPos != null)
        {
            transform.position = spawnPos;
        }
    }

    protected virtual void StartingAssignments()
    {
        enemyTarget = GameObject.FindWithTag("Player").transform;
        currentRoom = transform.parent.gameObject;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        spawnPos = transform.position;
        print("enemy spawy position = " + spawnPos);
    } 

    protected virtual void BaseStats()
    {
        // Base stats
        enemyHealth = 3;
        enemyName = "Unidentified Enemy";
        baseAttack = 1;
        moveSpeed = 1f;
        chaseRadius = 4f;
        attackRadius = 1f;
        attackSpeed = 2f;
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
            if (Vector2.Distance(enemyTarget.position, transform.position) >= attackRadius)
            {
                movement = enemyTarget.position - transform.position;
            }

            else
            {
                Attack();
            }
        }
        else
        {
            movement = Vector2.zero;
        }
    }

    private static void Attack()
    {
        print("attack");
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
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("enemy has hit player");
        }        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }

}
