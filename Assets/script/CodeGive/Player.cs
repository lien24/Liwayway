using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(20); // Adjust the damage value as needed
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Prevent health from going below 0
        healthBar.SetHealth(currentHealth);

        // Optionally, add feedback here, e.g., screen shake or sound
        if (currentHealth == 0)
        {
            // Handle player death (e.g., game over logic)
        }
    }
}
