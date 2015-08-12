using System;
using System.Collections.Generic;
using UnityEngine;

class HiveBuff : RuneBuff
{
    // Percentage chance of effect happening
    private int summonChance = 25;

    public override void OnBegin(Transform chr)
    {
        base.OnBegin(chr);

        // Subscribe to the event using C# 2.0 syntax
        PublisherBox.onHitPub.RaiseOnHitEvent += HandleOnHitEvent;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Unsubscribe to the event using C# 2.0 syntax
        PublisherBox.onHitPub.RaiseOnHitEvent -= HandleOnHitEvent;
    }

    // Define what actions to take when event is raised
    private void HandleOnHitEvent(object sender, POnHitEventArgs e)
    {
        // Roll a random chance
        int chance = UnityEngine.Random.Range(1, 100);

        // If the chance passes
        if (chance <= this.summonChance)
        {
            // Do the effect (summon)
            Debug.Log("Place holder summon!");
        }
    }

}
