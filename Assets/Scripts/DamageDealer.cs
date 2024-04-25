using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int baseDamage = 10;
    [SerializeField] int baseSpeed = 10;

    float damageMultiplier = 1f;
    bool piercingEnabled = false;
    [SerializeField] bool isPlayerBullet = false;

    public int GetDamage()
    {
        return Mathf.RoundToInt(baseDamage * damageMultiplier);
    }

    public void ApplyPowerUpEffects(float firingRate, bool spreadShotEnabled, bool piercingBulletsEnabled, Vector3 direction, float projectileSpeed)
    {
        // Apply damage multiplier based on power-up effects
        if (firingRate > 1f)
        {
            damageMultiplier *= 2f; // Example: Double damage during rapid fire
        }
        if (spreadShotEnabled)
        {
            damageMultiplier *= 0.5f; // Example: Half damage for spread shot
        }
        if (piercingBulletsEnabled)
        {
            piercingEnabled = true; // Enable piercing bullets
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable damagableTarget;
        if (isPlayerBullet && other.CompareTag("Player")) return;
        if (!isPlayerBullet && !other.CompareTag("Player")) return;
        if(other.TryGetComponent<IDamagable>( out damagableTarget))
        {
            damagableTarget.TakeDamage(GetDamage());
            if (!piercingEnabled)
            {
                // Destroy the projectile if piercing is not enabled
                Destroy(gameObject);
            }
        }


    }
}
