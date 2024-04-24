using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    Health health;
    Shooter shooter;
    Pathfinder pathfinder;

    bool isMiniBoss;

    public event EventHandler OnBossDestroyed;

    private void Start()
    {
        health = GetComponent<Health>();
        shooter = GetComponent<Shooter>();
        pathfinder = GetComponent<Pathfinder>();
        Debug.Log(pathfinder);
    }



    public void Spawn(WaveConfigSO waveConfigSO, EnemySpawner enemySpawner, Transform endPosition = null)
    {
        if(pathfinder == null) 
        {
            pathfinder = GetComponent<Pathfinder>();
            Debug.LogWarning("Pathfinder is null   " + this.name); 
        }
        pathfinder.Init(waveConfigSO, enemySpawner, endPosition);
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
}
