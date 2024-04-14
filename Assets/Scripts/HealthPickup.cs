using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    [SerializeField] int healAmount;
    [SerializeField] ParticleSystem healthParticle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.TryGetComponent<Health>(out Health health))
        { 
            if (health.GetIsPlayer())
            {
                health.HealPlayer(healAmount);
                if(healthParticle != null)
                {
                    Instantiate(healthParticle, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }
}
