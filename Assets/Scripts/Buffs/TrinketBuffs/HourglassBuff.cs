using UnityEngine;

class HourglassBuff : TrinketBuff
{
    // Flag to mark if this effect is still up for this floor
    private bool active = true;

    public override void OnBegin(Transform chr)
    {
        base.OnBegin(chr);

        // Subscribe to the event using C# 2.0 syntax
        PublisherBox.onDeathPub.RaiseOnDeathEvent += HandleOnDeathEvent;
        // PublisherBox.onNewFloorPub.RaiseOnNewFloorEvent += HandleOnNewFloorEvent;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Unsubscribe when the buff is removed
        PublisherBox.onDeathPub.RaiseOnDeathEvent -= HandleOnDeathEvent;
        // PublisherBox.onNewFloorPub.RaiseOnNewFloorEvent -= HandleOnNewFloorEvent;
    }

    // Define what actions to take when event is raised
    private void HandleOnDeathEvent(object sender, POnDeathEventArgs e)
    {
        // IF this effect hasn't happened once on this floor
        if (active)
        {
            // Do the effect (Revive)
            Debug.Log("Revive!");

            // Probably play an animation here and make the player invincible

            // !!!!!!!!!!!!!!!!!!!!!!

            // Restore all health
            stats.Health = stats.MaxHealth;
            // Revive from the dead
            stats.Dead = false;

            // Stop this effect from happening until it is reactivated (next floor)
            active = false;
        }
    }

    /*
    private void HandleOnNewFloorEvent(object sender, POnNewFloorEventArgs e)
    {
        Debug.Log("Regaining revive!");

        active = true;
    }
    */
}
