using UnityEngine;

class PerfectionistBuff : AttributeBuff
{

    // The damage multiplier to add
    private int multiplier = 2;
    // Flag to know when this buff is in effect
    private bool fullHP = false;

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Do an initial check to add the multiplier
        fullHP = stats.MaxHealth == stats.Health;
        if (fullHP)
        {
            stats.DamageMuliplier += multiplier;
        }

        // Subscribe to the OnHealthChangeEvent to check every time health changes
        PublisherBox.onHealthChangePub.RaiseOnHealthChangeEvent += HandleOnHealthChangeEvent;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove the multiplier if it is active
        if (fullHP)
        {
            stats.DamageMuliplier -= multiplier;
        }

        // Unsubscribe to the OnHealthChangeEvent
        PublisherBox.onHealthChangePub.RaiseOnHealthChangeEvent -= HandleOnHealthChangeEvent;
    }

    // Define what actions to take when event is raised
    private void HandleOnHealthChangeEvent(object sender, POnHealthChangeEventArgs e)
    {
        // Do the effect (Check multiplier)
        fullHP = stats.MaxHealth == stats.Health;

        if (fullHP)
        {
            stats.DamageMuliplier += multiplier;
        }
        else
        {
            stats.DamageMuliplier -= multiplier;
        }
    }
}
