using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DestroyEnemiesPickup : Pickup
{
    Enemy[] enemyList;
    public override void PickupEffect(Player player)
    {
        base.PickupEffect(player);
        enemyList = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemyList)
        {
            enemy.InstaDie();
        }
    }

    protected override void Update() { base.Update(); }
}
