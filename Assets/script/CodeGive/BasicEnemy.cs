using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // Optional: Trigger death animation or effects here
            Destroy(gameObject);
        }
    }
}
