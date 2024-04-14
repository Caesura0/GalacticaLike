using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("General Settings")]

    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake = false;
    [SerializeField] bool isPlayer = false;
    [SerializeField] int pointsAwarded = 0;

    [Space]
    [Header("Audio Settings")] 

    [SerializeField] AudioClip hitSFX;
    [SerializeField] [Range(0f, 1f)] float hitVolume = 1f;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }


    public int GetHealth()
    {
        return health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
        }
            
     }


    public void HealPlayer(int healAmount)
    {
        this.health += healAmount;
    }


    private void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    private void TakeDamage(int damage)
    {
        audioPlayer.PlayOneShotClip(hitSFX, hitVolume);
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (!CompareTag("Player"))
        {
            scoreKeeper?.SetScore(pointsAwarded);
        }
        else
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    void PlayHitEffect()
    {
        if (hitEffect)
        {
            ParticleSystem particleInstance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(particleInstance.gameObject, particleInstance.main.duration + particleInstance.main.startLifetime.constantMax);
        }
    }


    public bool GetIsPlayer()
    {
        return isPlayer;
    }
}
