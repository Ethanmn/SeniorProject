using System;
using System.Collections.Generic;
using UnityEngine;

class ThirstBuff : RuneBuff
{ 
    // Percentage chance of effect happening
    private int chanceScale = 15;

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
        if (chance <= chanceScale * level)
        {
            if (level <= 2 && level > 0)
            {
                // Do the effect (heal)
                chr.GetComponent<HeroStats>().Health += 1;
                Debug.Log("Healed! Health is now " + chr.GetComponent<HeroStats>().Health);
            }
            else if (level >= 3)
            {
                // Do the SUPER effect (heal for damage dealt)
                chr.GetComponent<HeroStats>().Health += e.Damage;
            }
        }
    }

}
