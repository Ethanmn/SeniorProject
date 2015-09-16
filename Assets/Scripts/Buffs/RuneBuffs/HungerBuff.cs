using System;
using System.Collections.Generic;
using UnityEngine;

class HungerBuff : RuneBuff
{
    // Damage cap per level
    private int damageScale = 2;
    // Current amount of bonus damage
    private int bonusDamage = 0;
    // Total max bonus damage
    private int bonusDamageMax;

    public override void OnBegin(Transform chr)
    {
        base.OnBegin(chr);

        bonusDamageMax = damageScale * level;

        // Subscribe to the event using C# 2.0 syntax
        // Event that triggers when the player kills an enemy
        PublisherBox.onHitPub.RaiseOnHitEvent += HandleOnHitEvent;
        PublisherBox.onHurtPub.RaiseOnHurtEvent += HandleOnHurtEvent;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Unsubscribe when the buff is removed
        PublisherBox.onHitPub.RaiseOnHitEvent -= HandleOnHitEvent;
        PublisherBox.onHurtPub.RaiseOnHurtEvent -= HandleOnHurtEvent;
    }

    // Define what actions to take when event is raised
    private void HandleOnHitEvent(object sender, POnHitEventArgs e)
    {
        // IF the enemy was killed
        if (e.Damage >= e.Enemy.GetComponent<MobStats>().Health)
        {
            // Do the effect (Add bonus)
            if (bonusDamage < bonusDamageMax)
            {
                bonusDamage++;
                stats.BonusDamage++;
                Debug.Log("MOAR DAMAGE!! " + bonusDamage);
            }
        }
        else
            Debug.Log("Didn't kill!");
    }

    // Define what actions to take when event is raised
    private void HandleOnHurtEvent(object sender, POnHurtEventArgs e)
    {
        // Do the effect (Remove bonus)
        // IF level 1 or 2, lose ALL stacks
        if (level <= 2 && level > 0)
        {
            stats.BonusDamage -= bonusDamage;
            bonusDamage = 0;
        }
        // ELSE IF level 3, lose HALF of stacks
        else if (level >= 3)
        {
            stats.BonusDamage -= bonusDamage / 2;
            bonusDamage = bonusDamage / 2;
        }

        Debug.Log("Bonus damage lost!");
    }
}
