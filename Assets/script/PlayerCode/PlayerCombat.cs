using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform AttackPoint;
    public LayerMask EnemyLayer;

    public float AttackRange = 0.5f;
    public int attackDamage = 40;

    public int maxHealth = 100;      // Maximum player health
    public int currentHealth;       // Current player health

    public float attackRate = 4f;
    float nextAttackTime = 0f;

    private Rigidbody rb;
    private Collider playerCollider;

    public HealthBars healthBar;      // Reference to the health bar UI

    public Canvas healthBarCanvas;   // Reference to the Canvas containing the health bar UI

    public float deathDelay = 3f;  // Time delay before restarting the scene

    void Start()
    {
        currentHealth = maxHealth;   // Initialize player health

        healthBar.UpdateHealth((float)currentHealth / maxHealth);  // Initialize the health bar


        // Get Rigidbody and Collider components
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();

        
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        // Play an attack animation
        animator.SetTrigger("Attack");

        // Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(AttackPoint.position, AttackRange, EnemyLayer);

        // Damage them
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    // This function allows the player to take damage from enemies
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play hurt animation
        animator.SetTrigger("Hurt");

        // Update the health bar UI
        healthBar.UpdateHealth((float)currentHealth / maxHealth);

        // Check if player is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Play die animation
        animator.SetBool("IsDead", true);

        // Disable player movement and attacks
        GetComponent<PlayerMove>().enabled = false;
        this.enabled = false;

      


        // Disable the enemy's collider to stop further hits
        playerCollider.enabled = false;

        // Disable gravity and make the Rigidbody kinematic so it doesn't fall
        rb.useGravity = false;
        rb.isKinematic = true;

        // Disable this script to stop further enemy actions
        this.enabled = false;

        // Hide the health bar canvas
        healthBarCanvas.enabled = false;  // Disable the health bar UI

        //SceneManager.LoadScene(2);

        // Start the coroutine to delay scene loading
        StartCoroutine(LoadRetrySceneWithDelay());
    }

    // Coroutine to delay scene loading
    IEnumerator LoadRetrySceneWithDelay()
    {
        yield return new WaitForSeconds(deathDelay);  // Wait for the specified delay

        // Load the retry scene (or replace 2 with the actual retry scene index)
        SceneManager.LoadScene(2);
    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null) return;

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
