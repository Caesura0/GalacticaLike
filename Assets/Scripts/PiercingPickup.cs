using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingPickup : Pickup
{
    public override void PickupEffect(Player player)
    {
        base.PickupEffect(player);
        player.Shooter.ActivatePowerUp(PowerUpType.Piercing);
    }

    protected override void Update() { base.Update(); } 
}
