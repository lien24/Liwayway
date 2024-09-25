using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject player; // The player object
    private Transform playerPos; // Player position
    public float aggroRange = 10f; // Distance within which the enemy starts chasing
    public float speedEnemy = 3f; // Speed of the enemy while patrolling
    public float chaseSpeed = 5f; // Speed of the enemy while chasing
    public Transform[] patrolPoints; // Array of patrol points
    private int currentPatrolIndex = 0; // Current patrol point index
    private Animator enemyAnim; // Animator component

    void Start()
    {
        playerPos = player.GetComponent<Transform>();
        enemyAnim = GetComponent<Animator>();
        if (patrolPoints.Length > 0)
        {
            transform.position = patrolPoints[currentPatrolIndex].position;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerPos.position);

        if (distanceToPlayer <= aggroRange)
        {
            // Start chasing the player
            ChasePlayer();
        }
        else
        {
            // Return to patrol if not chasing
            Patrol();
        }
    }

    void ChasePlayer()
    {
        // Set animator parameter to indicate chasing
        enemyAnim.SetBool("aggro", true);

        // Move towards the player with chase speed
        transform.position = Vector3.MoveTowards(transform.position, playerPos.position, chaseSpeed * Time.deltaTime);
    }

    void Patrol()
    {
        // Set animator parameter to indicate patrolling
        enemyAnim.SetBool("aggro", false);

        if (patrolPoints.Length == 0)
            return;

        // Move towards the current patrol point with patrol speed
        Transform targetPatrolPoint = patrolPoints[currentPatrolIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPatrolPoint.position, speedEnemy * Time.deltaTime);

        // Check if the enemy has reached the patrol point
        if (Vector3.Distance(transform.position, targetPatrolPoint.position) <= 0.1f)
        {
            // Move to the next patrol point
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }
}
