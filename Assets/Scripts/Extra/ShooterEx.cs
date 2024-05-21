using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEx : MonoBehaviour
{
    [Header("General")]

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 1f;
    [SerializeField] private int burstCount;
    [SerializeField] private int projectilesPerBurst;
    [SerializeField][Range(0, 359)] private float angleSpread;
    [SerializeField] private float startingDistance = 0.1f;
    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private float restTime = 1f;
    [SerializeField] private bool stagger;
    [Tooltip("Stagger must be enabled for oscillate to function properly.")]
    [SerializeField] private bool oscillate;




    [Space]
    [Header("Audio Settings")]
    [SerializeField] AudioClip shootingSFX;
    [SerializeField][Range(0f, 1f)] float shootVolume = 1f;
    AudioPlayer audioPlayer;

    bool targetPlayer;

    Vector2 targetDir;

    public bool piercing = false;

    Player player;
    private bool isShooting = false;

    private void OnValidate()
    {
        if (projectilesPerBurst < 1) { projectilesPerBurst = 1; }
        if (burstCount < 1) { burstCount = 1; }
        if (timeBetweenBursts < 0.1f) { timeBetweenBursts = 0.1f; }
        if (restTime < 0.1f) { restTime = 0.1f; }
        if (startingDistance < 0.1f) { startingDistance = 0.1f; }
        if (angleSpread == 0) { projectilesPerBurst = 1; }
        if (bulletMoveSpeed <= 0) { bulletMoveSpeed = 0.1f; }
    }


    Vector3 target;



    private void Start()
    {
        player = FindObjectOfType<Player>();
        audioPlayer  = FindObjectOfType<AudioPlayer>();
        targetPlayer = true;

        SetTarget(ShootDirection.Down);

        StartCoroutine(FireContinuously());


    }

    public void Attack()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }


    IEnumerator FireContinuously()
    {
        while (true)
        {

             Attack();
            

            yield return new WaitForSeconds(baseFiringRate);
        }
    }




    private IEnumerator ShootRoutine()
    {
        isShooting = true;

        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;

        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

        if (stagger) { timeBetweenProjectiles = timeBetweenBursts / projectilesPerBurst; }

        for (int i = 0; i < burstCount; i++)
        {
            if (!oscillate)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }

            if (oscillate && i % 2 != 1)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }
            else if (oscillate)
            {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }


            for (int j = 0; j < projectilesPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);

                Vector3 shootDirection = pos - (Vector2)transform.position;
                FireProjectile(shootDirection,pos);
                //GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                //newBullet.transform.right = newBullet.transform.position - transform.position;
             
                //if (newBullet.TryGetComponent(out DamageDealer projectile))
                //{
                    
                //}

                currentAngle += angleStep;

                if (stagger) { yield return new WaitForSeconds(timeBetweenProjectiles); }
            }

            currentAngle = startAngle;

            if (!stagger) { yield return new WaitForSeconds(timeBetweenBursts); }
        }

        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }




    public void SetTarget(ShootDirection direction)
    {
        switch (direction)
        {
            case ShootDirection.Up:
                targetDir = Vector2.up;
                targetPlayer = false;
                break;
            case ShootDirection.Down:
                targetDir = Vector2.down;
                targetPlayer = false;
                break;
            case ShootDirection.Left:
                targetDir = Vector2.left;
                targetPlayer = false;
                break;
            case ShootDirection.Right:
                targetDir = Vector2.right;
                targetPlayer = false;
                break;
            case ShootDirection.Player:
                targetPlayer = true;
                break;
        }
    }



    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        
        Vector2 targetDirection;
        if (targetPlayer)
        {
             targetDirection = player.transform.position - transform.position;
        }
        else
        {
            targetDirection = targetDir;
        }

        //calcuates the angle between positive x axis(global) (0,1) and target direction
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0;
        if (angleSpread != 0)
        {
            angleStep = angleSpread / (projectilesPerBurst - 1);
            halfAngleSpread = angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }


    void FireProjectile(Vector3 direction, Vector3 pos)
    {
        GameObject projectile = Instantiate(bulletPrefab, pos, Quaternion.identity);
        projectile.GetComponent<DamageDealer>().ApplyPowerUpEffects(2f, false, piercing, direction, bulletMoveSpeed);

        Destroy(projectile, projectileLifetime);

        audioPlayer.PlayOneShotClip(shootingSFX, shootVolume);
    }



    public enum ShootDirection
    {
        Left,
        Right,
        Up,
        Down,
        Player
    }
}
