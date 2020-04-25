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
    CircleCollider2D col;
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
        col = GetComponent<CircleCollider2D>();
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
            //print("player has hit enemy");
            TakeDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //print("enemy has hit player");
            other.gameObject.GetComponent<PlayerMovement>().PlayerTakeDamage(attackDammage);
        }        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }

    void TakeDamage()
    {
        StartCoroutine(FlashWhite(matDefault, matFlashWhite, flashWhiteSpeed, flashWhiteDurration));
    }

    IEnumerator FlashWhite(Material matDefault, Material matFlashWhite, float flashWhiteSpeed, float flashWhiteDurration)
    {
        float flashWhiteDurrationTimer = 0f;
        float flashWhiteSpeedTimer = flashWhiteSpeed;
        while (flashWhiteDurrationTimer <= flashWhiteDurration)
        {
            flashWhiteDurrationTimer += Time.deltaTime;
            flashWhiteSpeedTimer += Time.deltaTime;
            if (flashWhiteSpeedTimer >= flashWhiteSpeed)
            {
                if (sr.material == matDefault)
                    sr.material = matFlashWhite;
                else
                    sr.material = matDefault;
                flashWhiteSpeedTimer = 0f;
                print("sr.material = " + sr.material);
            }
            yield return null;
        }
        sr.material = matDefault;
        StopCoroutine(FlashWhite(matDefault, matFlashWhite, flashWhiteSpeed, flashWhiteDurration));
    }

}
