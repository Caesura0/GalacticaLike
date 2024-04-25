using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadPickup : Pickup
{


    public override void PickupEffect(Player player)
    {
        base.PickupEffect(player);
        player.Shooter.ActivatePowerUp(PowerUpType.SpreadShot);
    }
    protected override void Update() { base.Update(); }
}
