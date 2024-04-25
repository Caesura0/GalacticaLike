using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] protected float pickupDropSpeed = 1;
    [SerializeField] protected float startYPosition = 12;
    [SerializeField] protected float minXPosition;
    [SerializeField] protected float maxXPosition;
    [SerializeField] protected AudioClip pickupSoundEffect;
    [SerializeField] protected ParticleSystem pickupParticles;


    public virtual void PickupEffect(Player player)
    {
        Debug.Log("pickup item");
        if(pickupParticles != null )
        {
            Instantiate(pickupParticles, transform.position, Quaternion.identity);
            
        }
        if(pickupSoundEffect != null )
        {
            AudioSource.PlayClipAtPoint(pickupSoundEffect, transform.position);
        }
        Destroy(gameObject);
    }

    protected virtual void Update()
    {

        transform.Translate(Vector3.down * pickupDropSpeed * Time.deltaTime);

        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    public void CreatePickup()
    {
        float xPosition = Random.Range(minXPosition, maxXPosition);
        Vector2 startPostion = new Vector2(xPosition, startYPosition);
        
        transform.position = startPostion;
    }
}
