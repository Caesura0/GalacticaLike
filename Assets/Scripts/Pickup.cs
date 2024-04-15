using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] protected float movespeed;
    [SerializeField] protected float startYPosition;
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


}
