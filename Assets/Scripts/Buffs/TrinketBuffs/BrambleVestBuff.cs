using System;
using System.Collections.Generic;
using UnityEngine;

class BrambleVestBuff : TrinketBuff
{
    // The amount of damage
    private int damage = 3;
    // Amount of health to add
    private int bonusHealth = 1;

    public override void OnBegin(Transform chr)
    {
        base.OnBegin(chr);

        // Subscribe to the event using C# 2.0 syntax
        PublisherBox.onHurtPub.RaiseOnHurtEvent += HandleOnHurtEvent;

        // Add some bonus max health to make up for having to get hit
        stats.BonusMaxHealth += bonusHealth;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Unsubscribe when the buff is removed
        PublisherBox.onHurtPub.RaiseOnHurtEvent -= HandleOnHurtEvent;

        // Remove bonus max health when unequiped
        stats.BonusMaxHealth -= bonusHealth;
    }

    // Define what actions to take when event is raised
    private void HandleOnHurtEvent(object sender, POnHurtEventArgs e)
    {
        // Do the effect (Hurt the enemy)
            Debug.Log("Spikes!");

            // Velocity at which the enemy will be knocked back
            Vector2 vel;

            // Find a vector from the player to the enemy
            Vector2 pPos = e.Hero.transform.position;
            Vector2 ePos = e.Enemy.transform.position;

            vel = (ePos - pPos).normalized * 3f;

            e.Enemy.GetComponent<MobController>().Hit(damage, chr, vel);
        
    }

}
