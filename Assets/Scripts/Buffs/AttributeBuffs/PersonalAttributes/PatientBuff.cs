using System;
using System.Collections.Generic;
using UnityEngine;

class PatientBuff : AttributeBuff
{
    // Damage cap per level
    private int damageScale = 1;
    // Current amount of bonus damage
    private int bonusDamage = 0;
    // Total max bonus damage
    private int bonusDamageMax = 3;
    // Timer for counting damage ticks
    private float timer = 0f;

    public override void OnBegin(Transform chr)
    {
        base.OnBegin(chr);

        // Subscribe to the event using C# 2.0 syntax
        // Event that triggers when the player kills an enemy
        PublisherBox.onAttackPub.RaiseOnAttackEvent += HandleOnAttackEvent;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Unsubscribe when the buff is removed
        PublisherBox.onAttackPub.RaiseOnAttackEvent -= HandleOnAttackEvent;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        // IF the bonus damage is less than max damage added
        if (bonusDamage < bonusDamageMax)
        {
            // Count up
            timer += Time.deltaTime;
            // For each second through 3, add +1 damage to bonusDamage
            if (timer >= 1)
            {
                // Increment bonus damage count
                bonusDamage += damageScale;
                // Add to bonus damage
                stats.BonusDamage += damageScale;

                // Reset timer
                timer = 0;

                Debug.Log("Patient " + bonusDamage);
            }
        }
    }

    // Define what actions to take when event is raised
    private void HandleOnAttackEvent(object sender, POnAttackEventArgs e)
    {
        // Do the effect (Remove bonus)
        stats.BonusDamage -= bonusDamage;
        bonusDamage = 0;
        timer = 0;

        //Debug.Log("Bonus damage lost!");
    }
}
