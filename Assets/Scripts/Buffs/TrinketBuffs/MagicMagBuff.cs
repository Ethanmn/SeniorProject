using UnityEngine;

class MagicMagnetBuff : TrinketBuff
{
    // The amount of bonus rune find MM gives (currently 25% base, with +8% to get to 33%)
    private int bonusRuneFind = 8;

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Simply add stats
        // Increase rune find chance
        stats.BonusRuneFind += bonusRuneFind;

    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove given stats
        // Increase rune find chance
        stats.BonusRuneFind -= bonusRuneFind;
    }
}
