using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    Health health;
    Shooter shooter;
    IMover iMover;

    bool isMiniBoss;

    public event EventHandler OnBossDestroyed;

    private void Start()
    {
        health = GetComponent<Health>();
        shooter = GetComponent<Shooter>();
        iMover = GetComponent<IMover>();

    }



    public void Spawn(WaveConfigSO waveConfigSO, EnemySpawner enemySpawner, Transform endPosition = null)
    {
        if(iMover == null) 
        {
            iMover = GetComponent<IMover>();
            //Debug.LogWarning("Pathfinder is null   " + this.name); 
        }
        iMover.Init(waveConfigSO, enemySpawner, endPosition);
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
        if (!isMiniBoss)
        {
            health.Die();
            // maybe some sort of implosion or circle that folds down into a line for each enemy....
        }

    }


    public void Die()
    {

        //death particles
        Destroy(gameObject);
    }
}
