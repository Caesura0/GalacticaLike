using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    Health health;
    Shooter shooter;

    private void Start()
    {
        health = GetComponent<Health>();
        shooter = GetComponent<Shooter>();
    }

    public void TakeDamage(int damage)
    {
        if (health != null)
        {
            health.TakeDamage(damage); // Call the Damage method of the Health component attached to the enemy
        }
        else
        {
            Debug.LogWarning("Health component not found on enemy!");
        }
    }

    public void InstaDie()
    {
        health.Die();
        // maybe some sort of implosion or circle that folds down into a line for each enemy....
    }
}
