using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{

    [SerializeField] int healAmount;

    public override void PickupEffect(Player player)
    {
        player.Health.HealPlayer(healAmount);

        base.PickupEffect(player);

    }

    protected override void Update() { base.Update(); }
}
