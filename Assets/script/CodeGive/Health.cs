using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int currentHealth, maxHealth;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    private bool isDead = false;

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead || sender.layer == gameObject.layer) return;

        currentHealth -= amount;

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            isDead = true;
            OnDeathWithReference?.Invoke(sender);
            Destroy(gameObject);
        }
    }
}
