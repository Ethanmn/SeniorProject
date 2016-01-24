using UnityEngine;

class PerfectionistBuff : AttributeBuff
{

    // The damage bonus to add
    private int bonusDam = 1;
    // Flag to know when this buff is in effect
    private bool fullHP = false;
    // Amount of damage already added
    private int damAdded = 0;

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Do an initial check to add the bonus
        fullHP = stats.MaxHealth == stats.Health;
        if (fullHP)
        {
            Debug.Log("FULL ATT");
            stats.BonusDamage += bonusDam;
            damAdded += bonusDam;
        }
        else
        {
            Debug.Log("NOT FULL ATT");
        }

        // Subscribe to the OnHealthChangeEvent to check every time health changes
        PublisherBox.onHealthChangePub.RaiseOnHealthChangeEvent += HandleOnHealthChangeEvent;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove the bonus if it is active
        if (fullHP)
        {
            stats.BonusDamage -= bonusDam;
            damAdded -= bonusDam;
        }

        // Unsubscribe to the OnHealthChangeEvent
        PublisherBox.onHealthChangePub.RaiseOnHealthChangeEvent -= HandleOnHealthChangeEvent;
    }

    // Define what actions to take when event is raised
    private void HandleOnHealthChangeEvent(object sender, POnHealthChangeEventArgs e)
    {
        // Do the effect (Check bonus)
        fullHP = stats.MaxHealth == stats.Health;

        if (fullHP)
        {
            if (damAdded < bonusDam)
            {
                Debug.Log("PERFECT");
                stats.BonusDamage += bonusDam;
                damAdded += bonusDam;
            }
        }
        else
        {
            if (damAdded >= bonusDam)
            {
                Debug.Log("LOst perfect");
                stats.BonusDamage -= bonusDam;
                damAdded -= bonusDam;
            }
        }
    }
}
