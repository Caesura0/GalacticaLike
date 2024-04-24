using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float paddingLeft = 5f;
    [SerializeField] float paddingRight = 5f;
    [SerializeField] float paddingTop = 5f;
    [SerializeField] float paddingBot = 5f;



    [SerializeField] SpriteRenderer playerShipImage;



    [SerializeField] float iFramesSeconds = 1f;




    Vector2 rawInput;
    Vector2 minimumBounds;
    Vector2 maximumBounds;

    Shooter shooter;
    Health health;

    bool isInvincible = false;

    public Health Health {  get { return health; } }
    public Shooter Shooter {  get { return shooter; } }

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
        health = GetComponent<Health>();

    }

    private void Start()
    {
        InitBounds();
    }



    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 delta = rawInput * Time.deltaTime * moveSpeed;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minimumBounds.x + paddingLeft, maximumBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minimumBounds.y + paddingBot, maximumBounds.y - paddingTop);
        transform.position = newPos ;
    }





    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Pickup pickup = collision.GetComponent<Pickup>();
        if(pickup != null)
        {
            pickup.PickupEffect(this);
        }

    }

    public void SetShieldActive()
    {
        health.SetShield(true);
    }



    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minimumBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maximumBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
        
    }

    public void TakeDamage(int damage)
    {
        
        //take damage and then have invincibily and flashing for time in 'iFramesSeconds'
        if (!isInvincible)
        {
            StartCoroutine(IFramesProcess());
            health.TakeDamage(damage);
        }
        //lets be fancy and pass a delegate back here if we die, and if so , active the base die logic along with specific player die logic
        //same thing on the enemy script
    }

    IEnumerator IFramesProcess()
    {
        // Enable invincibility
        isInvincible = true;

        // Flash the player sprite during invincibility
        StartCoroutine(FlashSprite());

        // Wait for the specified duration of invincibility
        yield return new WaitForSeconds(iFramesSeconds);

        // Disable invincibility after the duration is over
        isInvincible = false;
    }

    IEnumerator FlashSprite()
    {


        // Get the original color of the sprite
        Color originalColor = playerShipImage.color;

        // Define the colors for flashing
        Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        Color visibleColor = originalColor;

        // Flash the sprite for the duration of invincibility
        float flashDuration = 0.1f; // Adjust as needed
        while (isInvincible)
        {
            // Set the sprite color to transparent
            playerShipImage.color = transparentColor;

            // Wait for a short duration
            yield return new WaitForSeconds(flashDuration);

            // Set the sprite color back to visible
            playerShipImage.color = visibleColor;

            // Wait for a short duration
            yield return new WaitForSeconds(flashDuration);
        }

        // Reset the sprite color to its original color when invincibility ends
        playerShipImage.color = originalColor;
    }
}
