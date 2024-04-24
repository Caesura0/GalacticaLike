using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour, IDamagable
{
    [SerializeField] protected float dropSpeed = 2.5f;
    [SerializeField] protected float maxDropSpeed = 2.5f;
    [SerializeField] protected float minRotationSpeed = 10f;
    [SerializeField] protected float maxRotationSpeed = 45f;
    [SerializeField] protected int impactDamage = 10;
    [SerializeField] protected Transform visual;

    float rotationSpeed;
    Vector2 downMovement = Vector2.down;

    Health health;

    private void Start()
    {
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        health = GetComponent<Health>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(downMovement * dropSpeed * Time.deltaTime);
        visual.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + " collision");


    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log(collision.gameObject.name + " trigger");
    //    IDamagable player;
    //    if (collision.gameObject.TryGetComponent<IDamagable>(out player))
    //    {
    //        if (collision.CompareTag("Player"))
    //        {
    //            player.TakeDamage(impactDamage);
    //        }
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        IDamagable player;

        if (collision.gameObject.TryGetComponent<IDamagable>(out player))
        {
            if (collision.CompareTag("Player"))
            {
                player.TakeDamage(impactDamage);
            }
        }
    }




    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
    }
}
