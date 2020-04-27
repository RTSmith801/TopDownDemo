using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { idle, walk, attack, stagger}

public class EnemyBaseClass : MonoBehaviour
{
    public EnemyState currentState;    

    public int enemyHealth;
    public string enemyName;
    public int attackDamage;
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
    public bool canMove = true;
        
    Vector2 targetPosition = new Vector2 (0,0);
    Vector2 currentPosition = new Vector2 (0,0);



    private void Awake()
    {
        StartingAssignments();
        BaseStats();
    }

    protected virtual void StartingAssignments()
    {
        enemyTarget = GameObject.FindWithTag("Player").transform;
        currentRoom = transform.parent.gameObject;
        rb = GetComponent<Rigidbody2D>();        
        spawnPos = transform.position;        
    }

    private void OnEnable()
    {
        if (!isAlive || spawnPos == null)
            return;

        transform.position = spawnPos;
        currentState = EnemyState.idle;
        
    }

    protected virtual void BaseStats()
    {
        // Base stats
        enemyHealth = 3;
        enemyName = "Unidentified Enemy";
        attackDamage = 1;
        moveSpeed = 3f;
        chaseRadius = 4f;
        attackRadius = .1f;
        attackSpeed = 2f;
    }

    private void Update()
    {
        RecordTransformPositions();        
    }


    void RecordTransformPositions()
    {
        targetPosition = enemyTarget.position;
        currentPosition = transform.position;

        movement = (targetPosition - currentPosition);

        if (Vector2.Distance(targetPosition, currentPosition) <= chaseRadius)
            enemmyTargetWithinRadius = true;
        else
            enemmyTargetWithinRadius = false;
    }
    

    private static void Attack()
    {
        // Attack goes here
    }

    private void FixedUpdate()
    {
        if (isAlive && canMove && enemmyTargetWithinRadius && enemyTarget != null)
        {
            if (currentState != EnemyState.stagger)
                MoveEnemy();        
        }
    }

    private void MoveEnemy()
    {           

        if (Vector2.Distance(targetPosition, currentPosition) >= attackRadius)
        {
            ChangeState(EnemyState.walk);
            Vector2 temp = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(temp);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //print("enemy collided with player");
            other.gameObject.GetComponent<HealthManager>().TakeDamage(attackDamage);
        }        
    }

    void TakeDamage()
    {
        canMove = false;
        gameObject.GetComponent<HealthManager>().TakeDamage(attackDamage);
    }
    
    public void Death()
    {
        isAlive = false;        
    }

    void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
            currentState = newState;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }

}
