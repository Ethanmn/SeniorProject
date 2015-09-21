using UnityEngine;

class ButcherBuff : AttributeBuff
{
    // Amount to heal on kills
    private int heal = 1;

    // Number of kills needed to heal
    private int reqKills = 4;
    // Counter for kills
    private int counter = 0;

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Subscribe to the event using C# 2.0 syntax
        PublisherBox.onKillPub.RaiseOnKillEvent += HandleOnKillEvent;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Unsubscribe when the buff is removed
        PublisherBox.onKillPub.RaiseOnKillEvent -= HandleOnKillEvent;
    }

    private void HandleOnKillEvent(object sender, POnKillEventArgs e)
    {
        // IF the counter is less than the required kills
        if (counter < reqKills)
        {
            counter++;
        }
        // ELSE if the counter is equal to or greater than
        else
        {
            // WHILE counter is greater than or equal to required kills
            while (counter >= reqKills)
            {
                control.Heal(heal);
                counter -= reqKills;
            }
        }
    }
}
