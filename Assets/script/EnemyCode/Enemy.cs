using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    public int currentHealth;


    public float moveSpeed = 3f;
    public float attackRange = 1.5f;  // Distance at which enemy will attack
    public float detectionRange = 5f; // Distance at which enemy detects player

    public int attackDamage = 20;
    public LayerMask playerLayer;     // Layer to detect player
    public Transform player;          // Reference to the player

    private bool isAttacking = false;
    private float attackCooldown = 2f;
    private float nextAttackTime = 0f;

    private Rigidbody rb;
    private Collider enemyCollider;

    private Vector3 initialScale;  // Store the initial scale of the enemy

    public HealthBars healthBar;    // Reference to the enemy's health bar UI

    public Transform Canvas;        // Reference to the canvas (child object of the enemy)

    public Canvas healthBarCanvas;   // Reference to the Canvas containing the health bar UI



    // Add reference to the AudioSource component
    private AudioSource enemyAudio; 



    void Start()
    {
        currentHealth = maxHealth;

        // Initialize the health bar UI
        healthBar.UpdateHealth((float)currentHealth / maxHealth);


        // Get Rigidbody and Collider components
        rb = GetComponent<Rigidbody>();
        enemyCollider = GetComponent<Collider>();

        // Store the initial scale
        initialScale = transform.localScale;

        // Get the AudioSource component attached to the enemy
        enemyAudio = GetComponent<AudioSource>();

        // If player isn't assigned, try to find player by tag
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void Update()
    {
        if (player == null) return; // If player is not found, return

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the player is within detection range, move toward them
        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer > attackRange && !isAttacking)
            {
                MoveTowardsPlayer();
            }
            else if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
            {
                AttackPlayer();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
        else
        {
            // Stop movement and idle when player is out of detection range
            StopMoving();
        }
    }

    void MoveTowardsPlayer()
    {
        animator.SetBool("IsRunning", true);

        // Move toward the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Ensure movement is smooth
        rb.MovePosition(Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime));



        // Flip the enemy based on movement direction
        if (direction.x < 0) // Moving to the right
        {
            transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);  // Face right
            Canvas.localScale = new Vector3(1.5f, Canvas.localScale.y, Canvas.localScale.z);  // Set Canvas X scale to 1.5
        }
        else if (direction.x > 0) // Moving to the left
        {
            transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z); // Face left
            Canvas.localScale = new Vector3(-1.5f, Canvas.localScale.y, Canvas.localScale.z);  // Set Canvas X scale to -1.5
        }

    }

    void AttackPlayer()
    {
        animator.SetBool("IsRunning", false);

        // Check if player is in attack range
        Collider[] hitPlayers = Physics.OverlapSphere(transform.position, attackRange, playerLayer);

        if (hitPlayers.Length > 0)
        {
            isAttacking = true;

            // Play attack animation
            animator.SetTrigger("Attack");

            // Deal damage to player
            foreach (Collider player in hitPlayers)
            {
                player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
            }
        }

        // Reset attack state after attacking
        isAttacking = false;
    }


    void StopMoving()
    {
        // Stop running animation when idle
        animator.SetBool("IsRunning", false);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Update health bar UI
        healthBar.UpdateHealth((float)currentHealth / maxHealth);

        // Play hurt animation
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);

        // Disable the enemy's collider to stop further hits
        enemyCollider.enabled = false;

        // Disable gravity and make the Rigidbody kinematic so it doesn't fall
        rb.useGravity = false;
        rb.isKinematic = true;

        // Stop the enemy's audio when it dies
        if (enemyAudio != null)
        {
            enemyAudio.loop = false;  // Ensure looping is turned off
            enemyAudio.Stop();
        }


        // Disable this script to stop further enemy actions
        this.enabled = false;

        // Hide the health bar canvas
        healthBarCanvas.enabled = false;  // Disable the health bar UI
    }

    private void OnDrawGizmosSelected()
    {
        // Draw attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Draw detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

    }
}
