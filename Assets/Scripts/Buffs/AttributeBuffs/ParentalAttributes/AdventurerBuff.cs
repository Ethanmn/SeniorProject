using UnityEngine;

class AdventurerBuff : AttributeBuff
{
    // Amount of max health to add
    private int health = 1;
    // Amount of damage to add
    private int damage = 1;

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Simply add stats
        // Increase health
        stats.BonusMaxHealth += health;
        // Increase damage
        stats.BonusDamage += damage;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove given stats
        // Remove health
        stats.BonusMaxHealth -= health;
        // Remove damage
        stats.BonusDamage -= damage;
    }
}
