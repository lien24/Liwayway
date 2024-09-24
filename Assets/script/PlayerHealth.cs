using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;        // Player's max health
    private int currentHealth;         // Current health of the player
    public Animator animator;          // Reference to the Animator component

    void Start()
    {
        currentHealth = maxHealth;     // Initialize health to maximum
    }

    // Method for taking damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play the hurt animation
        animator.SetTrigger("Hurt");

        // Check if the player is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Play the die animation
        animator.SetTrigger("Die");

        // Disable player movement and interactions here (optional)
        GetComponent<PlayerMove>().enabled = false;
        this.enabled = false;

        // Add additional logic if needed (e.g., respawning, game over, etc.)
        Debug.Log("Player died!");
    }
}
