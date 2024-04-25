using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;

    [Space]
    [Header("Weapon Parameters")]
    [SerializeField] float baseFiringRate = 1f;
    [SerializeField] float rapidFiringRate = 3f;

    [Space]
    [Header("AI")]
    [SerializeField] bool useAi;
    [SerializeField] float fireRateVariance = 0.2f;
    [SerializeField] float minFireRate = 0.1f;

    [Space]
    [Header("Audio Settings")]
    [SerializeField] AudioClip shootingSFX;
    [SerializeField][Range(0f, 1f)] float shootVolume = 1f;

    [Space]
    [Header("Power-up Durations")]
    [SerializeField] float rapidFireDuration = 5f;
    [SerializeField] float spreadShotDuration = 60f;
    [SerializeField] float piercingDuration = 30f;


    [Space]
    [Header("SpreadShot Settings")]
    [SerializeField] int numProjectiles = 5; // Number of projectiles in the spread shot
    [SerializeField] float spreadAngle = 30f; // Angle between each projectile in the spread


    //Make this a property later
    [HideInInspector] public bool isFiring;

    Coroutine fireCoroutine;
    AudioPlayer audioPlayer;

    private float currentFiringRate;
    [SerializeField] bool spreadShotEnabled = false;
    [SerializeField] bool piercing = false;

    public PowerUpType powerUpType = PowerUpType.None;


    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        currentFiringRate = baseFiringRate;
    }

    private void Start()
    {
        if (useAi)
        {
            isFiring = true;
        }
    }

    private void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    public void ActivatePowerUp(PowerUpType powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUpType.RapidFire:
                currentFiringRate = rapidFiringRate;
                StartCoroutine(ResetFiringRateAfterDelay(rapidFireDuration));
                break;
            case PowerUpType.SpreadShot:
                spreadShotEnabled = true;
                StartCoroutine(DisableSpreadShotAfterDelay(spreadShotDuration));
                break;
            case PowerUpType.Piercing:
                piercing = true;
                StartCoroutine(DisablePiercingAfterDelay(piercingDuration));
                break;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            if (spreadShotEnabled)
            {
                // Spread shot behavior
                SpreadShot();
            }
            else
            {
                // Regular firing behavior
                FireProjectile(transform.up);
            }

            yield return new WaitForSeconds(currentFiringRate);
        }
    }


  void SpreadShot()
    {
        if (!useAi)
        {
            //player shooting upwards
            for (int i = 0; i < numProjectiles; i++)
            {
                float angle = transform.eulerAngles.z - (spreadAngle / 2) + (i * (spreadAngle / (numProjectiles - 1)));
                Vector3 direction = Quaternion.Euler(0, 0, angle) * transform.up;
                FireProjectile(direction);
            }
        }
        else
        {
            //enemies shooting downwards
            for (int i = 0; i < numProjectiles; i++)
            {
                float angle = transform.eulerAngles.z - (spreadAngle / 2) + (i * (spreadAngle / (numProjectiles - 1)));
                Vector3 direction = Quaternion.Euler(0, 0, angle) * -transform.up;
                FireProjectile(direction);
            }
        }


    }

    void FireProjectile(Vector3 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<DamageDealer>().ApplyPowerUpEffects(currentFiringRate, spreadShotEnabled, piercing, direction, projectileSpeed);

        Destroy(projectile, projectileLifetime);

        audioPlayer.PlayOneShotClip(shootingSFX, shootVolume);
    }


    IEnumerator ResetFiringRateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentFiringRate = baseFiringRate;
    }

    IEnumerator DisableSpreadShotAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        spreadShotEnabled = false;
    }

    IEnumerator DisablePiercingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        piercing = false;
    }
}

public enum PowerUpType
{
    RapidFire,
    SpreadShot,
    Piercing,
    None
}
