using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrig : MonoBehaviour
{
    //animation declared
    public Animator animator;


    public int MaxHealth = 100;
    public int CurrentHealth;
    // Reference to the Rigidbody and Collider components
    private Rigidbody rb;
    private Collider enemyCollider;



    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;

        // Get the Rigidbody and Collider components
        rb = GetComponent<Rigidbody>();
        enemyCollider = GetComponent<Collider>();
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        //play hurt animation
        animator.SetTrigger("Hurt");

        if (CurrentHealth < 0)
        {
            Die();
        }

    }

    void Die()
    {
        Debug.Log("die");

        //die animation 
        animator.SetBool("IsDead", true);

        // Disable the enemy's collider to stop further hits
        enemyCollider.enabled = false;

        // Disable gravity and make the Rigidbody kinematic so it doesn't fall
        rb.useGravity = false;
        rb.isKinematic = true;

        // Disable this script to stop further enemy actions
        this.enabled = false;
    }

 
}
