using UnityEngine;

class WingedBootsBuff : TrinketBuff
{
    // The amount of bonus movespeed
    private int bonusMoveSpeed = 3;

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Simply add stats
        // Increase movespeed
        stats.BonusSpeed += bonusMoveSpeed;

    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove given stats
        // Remove extra movespeed
        stats.BonusSpeed -= bonusMoveSpeed;
    }
}
