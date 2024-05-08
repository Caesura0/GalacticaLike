using Unity.VisualScripting;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float maxAngularSpeed = 120; // Maximum rotation speed in degrees per second
    public float maxTurnDamping = 2f; // Maximum damping factor for rotation
    public float minDistanceForMaxDamping = 5f; // Minimum distance for maximum damping
    public float turnAcceleration = 1f; // Rate of acceleration in damping
    public float missileLifetime = 10f;
    public int missileDamage = 30;


    [SerializeField] ParticleSystem explosion;

    private Rigidbody2D rb;
    private float currentMissileLife;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>().transform;
        currentMissileLife = missileLifetime;
    }

    private void Update()
    {

        if (currentMissileLife > 0)
        {
            currentMissileLife -= Time.deltaTime;
        }
        if (currentMissileLife <= 0f)
        {

            Explode();
        }
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is null, homing missile cannot navigate.");
            return;
        }

        // Calculate direction to the target
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();

        // Calculate angle towards the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Smoothly rotate towards the target with damping
        float distanceToTarget = Vector2.Distance(rb.position, target.position);
        float maxDamping = Mathf.Lerp(1f, maxTurnDamping, Mathf.Clamp01(distanceToTarget / minDistanceForMaxDamping));
        float currentDamping = Mathf.MoveTowards(maxDamping, maxTurnDamping, turnAcceleration * Time.fixedDeltaTime);
        float rotateAmount = Mathf.MoveTowardsAngle(rb.rotation, angle, maxAngularSpeed * Time.fixedDeltaTime * currentDamping);
        rb.MoveRotation(rotateAmount);

        // Move missile forward with constant speed
        rb.velocity = transform.up * speed;
    }


    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        if (collision.TryGetComponent<Player>(out player))
        {
            player.TakeDamage(missileDamage);
            Explode();
        }
    }


}
