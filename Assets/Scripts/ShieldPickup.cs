using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : Pickup
{

    public override void PickupEffect(Player player)
    {
        player.SetShieldActive();
        base.PickupEffect(player);
    }
}
