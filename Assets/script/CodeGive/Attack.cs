using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 5.0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Ensure the Animator is linked
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        // Trigger the attack animation
        animator.SetTrigger("Attack");

        // Detect enemies within range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                BasicEnemy enemy = hitCollider.GetComponent<BasicEnemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}
