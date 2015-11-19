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
        PublisherBox.onKillPub.RaiseOnKillEvent += HandleOnKillEvent;
        PublisherBox.onHurtPub.RaiseOnHurtEvent += HandleOnHurtEvent;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Unsubscribe when the buff is removed
        PublisherBox.onKillPub.RaiseOnKillEvent -= HandleOnKillEvent;
        PublisherBox.onHurtPub.RaiseOnHurtEvent -= HandleOnHurtEvent;
    }

    protected override void AddStack()
    {
        base.AddStack();

        bonusDamageMax = damageScale * level;
    }

    // Define what actions to take when event is raised
    private void HandleOnKillEvent(object sender, POnKillEventArgs e)
    {
        // Do the effect (Add bonus)
        if (bonusDamage < bonusDamageMax)
        {
            bonusDamage++;
            stats.BonusDamage++;
            Debug.Log("Hunger " + bonusDamage);
        }
        else
        {
            Debug.Log("Max damage " + bonusDamage);
        }
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
