using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float paddingLeft = 5f;
    [SerializeField] float paddingRight = 5f;
    [SerializeField] float paddingTop = 5f;
    [SerializeField] float paddingBot = 5f;




    Vector2 rawInput;
    Vector2 minimumBounds;
    Vector2 maximumBounds;

    Shooter shooter;
    Health health;



    public Health Health {  get { return health; } }

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
            shooter.isFireing = value.isPressed;
        }
        
    }


}
