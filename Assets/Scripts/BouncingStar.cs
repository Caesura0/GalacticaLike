using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingStar : MonoBehaviour
{
    [SerializeField] protected Transform visual;
    [SerializeField] protected float rotationSpeed = 75f;
    [SerializeField] protected float movementSpeed = 15f;
    [SerializeField] protected float velocityBounceRandomness = 2f;

    Rigidbody2D rb;
    Vector2 velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = Random.insideUnitCircle.normalized * movementSpeed;
        rb.velocity = velocity;
    }

    void Update()
    {
        visual.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0f || screenPosition.x > Screen.width || screenPosition.x < 0f)
        {
            screenPosition.x = Mathf.Clamp(screenPosition.x, 0f, Screen.width);
            screenPosition.y = Mathf.Clamp(screenPosition.y, 0f, Screen.height);
            Vector3 newWorldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            transform.position = new Vector2(newWorldPosition.x, newWorldPosition.y);
            Bounce();
        }
    }

    private void Bounce()
    {
        // Get the current velocity
        Vector2 currentVelocity = rb.velocity;

        // Add a small random offset to each component of the velocity
        Vector2 modifiedVelocity = new Vector2(
            currentVelocity.x + Random.Range(-velocityBounceRandomness, velocityBounceRandomness),
            currentVelocity.y + Random.Range(-velocityBounceRandomness, velocityBounceRandomness)
        );

        // Normalize the modified velocity vector
        modifiedVelocity.Normalize();

        // Scale the normalized velocity to the magnitude of the original velocity
        modifiedVelocity *= currentVelocity.magnitude;

        // Assign the modified velocity back to the Rigidbody
        rb.velocity = -modifiedVelocity;
    }
}
