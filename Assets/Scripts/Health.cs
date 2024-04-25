using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("General Settings")]

    [SerializeField] int maxHealth = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake = false;
    [SerializeField] bool isPlayer = false;
    [SerializeField] int pointsAwarded = 0;
    [SerializeField] float shieldMaxTime = 15f;
    float shieldCurrentTimer = 0;

    [Space]
    [Header("Audio Settings")] 

    [SerializeField] AudioClip hitSFX;
    [SerializeField] [Range(0f, 1f)] float hitVolume = 1f;
    [SerializeField] GameObject shieldSprite;



    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    bool shieldActive;

    int currentHealth;

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        currentHealth = maxHealth;
    }

    private void Start()
    {
        if(isPlayer)
        {
            shieldSprite.SetActive(false);
        }
    }
    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return currentHealth;
    }

    public float GetHealthNormalized() => (float)currentHealth / (float)maxHealth;

    private void Update()
    {
        if (shieldActive )
        {
            shieldCurrentTimer -= Time.deltaTime;
            if(shieldCurrentTimer <= 0)
            {
                SetShield(false);
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
    //    if (damageDealer != null)
    //    {
    //        TakeDamage(damageDealer.GetDamage());

    //        //damageDealer.Hit();
    //    }
    //}

public void HealPlayer(int healAmount)
    {
        
        this.currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
    }


    private void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    public void TakeDamage(int damage)
    {
        if (!shieldActive)
        {
            audioPlayer.PlayOneShotClip(hitSFX, hitVolume);
            ShakeCamera();
            PlayHitEffect();
            int effectiveDamage = Mathf.Min(damage, currentHealth);
            currentHealth -= effectiveDamage;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
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

    private void PlayHitEffect()
    {
        if (hitEffect)
        {
            ParticleSystem particleInstance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(particleInstance.gameObject, particleInstance.main.duration + particleInstance.main.startLifetime.constantMax);
        }
    }

    public void SetShield(bool shield)
    {
        shieldSprite.SetActive(shield);
        shieldActive = shield;
        shieldCurrentTimer = shieldMaxTime;
    }

    public bool GetIsPlayer()
    {
        return isPlayer;
    }
}
