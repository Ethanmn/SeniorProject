using System;
using System.Collections.Generic;
using UnityEngine;

class ThornBuff : RuneBuff
{
    // The amount of damage per level
    private int damageScale = 2;

    public override void OnBegin(Transform chr)
    {
        base.OnBegin(chr);

        // Subscribe to the event using C# 2.0 syntax
        PublisherBox.onHurtPub.RaiseOnHurtEvent += HandleOnHurtEvent;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Unsubscribe when the buff is removed
        PublisherBox.onHurtPub.RaiseOnHurtEvent -= HandleOnHurtEvent;
    }

    // Define what actions to take when event is raised
    private void HandleOnHurtEvent(object sender, POnHurtEventArgs e)
    {
        // Do the effect (Hurt the enemy)
        if ((level <= 2 && e.Hero.GetComponent<HeroController>().State.GetType().Equals(typeof(HeroStateIdle))) ||
            level >= 3)
        {
            Debug.Log("Spikes!");

            // Velocity at which the enemy will be knocked back
            Vector2 vel;

            // Find a vector from the player to the enemy
            Vector2 pPos = e.Hero.transform.position;
            Vector2 ePos = e.Enemy.transform.position;

            vel = (ePos - pPos).normalized * 3f;

            e.Enemy.GetComponent<MobController>().Hit(damageScale * level, chr, vel);
        }
        
        
    }

}
