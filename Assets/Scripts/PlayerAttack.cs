using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    Animator animator;  
    PlayerControls XBoxControllerInput;
    PlayerMovement playerMovement;
    public LayerMask enemyLayers;
    public Transform attackPoint;
    public Transform attackPointOffset;
    // Conversion from Vector 2 to Vector 3 for quaternion rotation.
    Vector3 lastLookDirection;
    GameManager gm;
    // I'd like to set all player stats from a single script. 
    int attackDamage = 1;
    public float attackRange = .5f;
    float attackRate = .2f;
    float nextAttackTime = 0f;
    public float attackReach = 1f;
    
    public bool canAttack = true;
    float knockbackStrength = 10f;
    float knockbackTime = .1f;

    // Use Awake(), OnEnable(), and OnDisable() to identify controller input
    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        XBoxControllerInput = new PlayerControls();

        XBoxControllerInput.Gameplay.X.performed += ctx => PlayerAttackCalled();
        XBoxControllerInput.Gameplay.RightBumper.performed += ctx => PlayerAttackCalled();
    }

    private void OnEnable()
    {
        XBoxControllerInput.Gameplay.Enable();
    }

    private void OnDisable()
    {
        XBoxControllerInput.Gameplay.Disable();
    }

    private void Start()
    {   
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        canAttack = true;
    }

    private void Update()
    {
        lastLookDirection = playerMovement.lastLookDirection;
    }

    private void LateUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {               
        Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, lastLookDirection);
        desiredRotation = Quaternion.Euler(0, 0, desiredRotation.eulerAngles.z + 180);
        attackPointOffset.rotation = Quaternion.RotateTowards(attackPointOffset.rotation, desiredRotation, 300);    
    }

    private void PlayerAttackCalled()
    {
        if (Time.time >= nextAttackTime && canAttack)
        {
            animator.SetTrigger("Attack");
            SoundCue();
            playerMovement.canMove = false;
            nextAttackTime = Time.time + attackRate;

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach(Collider2D enemy in hitEnemies)
            {
                //print("hit " + enemy.name);
                enemy.GetComponent<HealthManager>().TakeDamage(attackDamage);
                Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    print("Knockback collision has been called on " + enemy.gameObject.name);
                    rb.GetComponent<EnemyBaseClass>().currentState = EnemyState.stagger;
                                        
                    Vector2 direction = enemy.transform.position - transform.position;
                    //direction.y = 0;

                    rb.AddForce(direction.normalized * knockbackStrength, ForceMode2D.Impulse);
                    StartCoroutine(Knocked(rb));
                }
            }
        }
    }

    private IEnumerator Knocked(Rigidbody2D enemy)
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

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    // This method exists on multiple scripts and is called in the animator.
    public void AnimationExit()
    {
        //print("This was called from Player attack");
        //isAttacking = false;
    }

    public void Death()
    {
        canAttack = false;
    }
}
