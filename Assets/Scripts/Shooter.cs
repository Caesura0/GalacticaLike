using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 1f;
    [Space]
    [Header("AI")]
    [SerializeField] bool useAi;
    [SerializeField] float fireRateVariance = 0.2f;
    [SerializeField] float minFireRate = 0.1f;

    [Space]
    [Header("Audio Settings")]
    [SerializeField] AudioClip shootingSFX;
    [SerializeField] [Range(0f, 1f)] float shootVolume = 1f;

    [HideInInspector] public bool isFireing;

    Coroutine fireCoroutine;

    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();

    }


    private void Start()
    {
        if (useAi)
        {
            isFireing = true;
        }
    }

    private void Update()
    {
        Fire();
    }



    void Fire()
    {
        if (isFireing && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFireing && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }

    }

    public float GetRandomSpawnTime()
    {
        if (useAi)
        {
            float randomFire = UnityEngine.Random.Range(baseFiringRate - fireRateVariance, baseFiringRate + fireRateVariance);
            return Mathf.Clamp(randomFire, minFireRate, float.MaxValue);
        }
        else
        {
            return baseFiringRate;
        }
    }


    IEnumerator FireContinuously()
    {
        while(true) 
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }
            Destroy(projectile, projectileLifetime);

            //audioPlayer.PlayShootingClip();
            audioPlayer.PlayOneShotClip(shootingSFX, shootVolume);

            yield return new WaitForSeconds(GetRandomSpawnTime());
        }

    }





}
