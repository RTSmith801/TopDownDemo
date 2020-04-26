using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{    
    public int enemyHealth;
    public string enemyName;
    public int attackDammage;
    public float moveSpeed;
    public float chaseRadius;
    public float attackRadius;
    public float attackSpeed;

    GameObject currentRoom;    
    Rigidbody2D rb;
    Vector2 movement = new Vector2(0, 0);
    public Vector2 spawnPos = new Vector2();
    public Transform enemyTarget;
    public bool enemmyTargetWithinRadius = false;
    public bool isAlive = true;

    public SpriteRenderer sr;
    public Material matDefault;
    public Material matFlashWhite;
    public float flashWhiteSpeed = 0.05f;
    public float flashWhiteDurration = .5f;

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
        spawnPos = transform.position;
        //print("enemy spawy position = " + spawnPos);

        sr = GetComponent<SpriteRenderer>();
        matDefault = sr.material;
        matFlashWhite = Resources.Load("FlashWhite", typeof(Material)) as Material;
        //Invoke ("Test", 2f);
    }

    private void Test()
    {
        sr.material = null;
        sr.material = matFlashWhite;
        print("sr.material = " + sr.material);
        Invoke("Test2", 2f);
    }

    private void Test2()
    {
        sr.material = null;
        sr.material = matDefault;
        print("sr.material = " + sr.material);
        Invoke("Test", 2f);
    }

    protected virtual void BaseStats()
    {
        // Base stats
        enemyHealth = 3;
        enemyName = "Unidentified Enemy";
        attackDammage = 1;
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
        if (enemyTarget == null)
        {
            movement = Vector2.zero;
            return;
        }

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
        // Attack goes here
    }

    private void FixedUpdate()
    {
        if (!isAlive)
            return;

        MoveEnemy();
    }

    private void MoveEnemy()
    {
        if (!enemmyTargetWithinRadius)
            return;

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }


    // move to attack script.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //print("enemy collider has hit player");
            other.gameObject.GetComponent<HealthManager>().TakeDamage(attackDammage);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //print("enemy collided with player");
            other.gameObject.GetComponent<HealthManager>().TakeDamage(attackDammage);
        }        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }

    void TakeDamage()
    {
        gameObject.GetComponent<HealthManager>().TakeDamage(attackDammage);
    }
    
    public void Death()
    {
        isAlive = false;        
    }


}
