using UnityEngine;

class WeaverBuff : AttributeBuff
{
    // Amount of max move speed to work
    private int find = 25;

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Simply add stats
        // Increase rune find chance
        stats.BonusRuneFind += find;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove given stats
        // Decrease rune find chance
        stats.BonusRuneFind -= find;
    }
}
