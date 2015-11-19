using System;
using System.Collections.Generic;
using UnityEngine;

class EnrageBuff : RuneBuff
{
    // How much damage per level the buff gains
    int damageScale = 1;

    public override void OnBegin(Transform chr)
    {
        base.OnBegin(chr);

        // Subscribe to the event using C# 2.0 syntax
        PublisherBox.onHealthChangePub.RaiseOnHealthChangeEvent += HandleOnHealthChangeEvent;

        // Do the effect (Gain damage)
        if ((stats.Health <= stats.MaxHealth / 3 && level <= 2 && level > 0) ||
            (stats.Health <= stats.MaxHealth / 2 && level >= 3))
        {

            stats.EnrageDamage = damageScale * level;
        }
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Unsubscribe when the buff is removed
        PublisherBox.onHealthChangePub.RaiseOnHealthChangeEvent -= HandleOnHealthChangeEvent;
    }

    protected override void AddStack()
    {
        base.AddStack();

        // Do the effect (Gain damage)
        if ((stats.Health <= stats.MaxHealth / 3 && level <= 2 && level > 0) ||
            (stats.Health <= stats.MaxHealth / 2 && level >= 3))
        {

            stats.EnrageDamage = damageScale * level;
        }
    }

    // Define what actions to take when event is raised
    private void HandleOnHealthChangeEvent(object sender, POnHealthChangeEventArgs e)
    {
        
        // Do the effect (Gain damage)
        // If the level is <= 2, then 1/3 health is required, min 1 health
        // If the level is 3, then 1/2 damage is required, min 1 health
        if ((stats.Health <= (Math.Max(1, stats.MaxHealth / 3)) && level <= 2 && level > 0) ||
            (stats.Health <= (Math.Max(1, stats.MaxHealth / 2)) && level >= 3))
        {
            Debug.Log("Enraged!");
            stats.EnrageDamage = damageScale * level;
        }
        //  If above the threshold, no damage is added
        else
        {
            Debug.Log("Unraged!");
            stats.EnrageDamage = 0;
        }
    }

}
