// Stat doesn't exist yet!

using UnityEngine;

class FletcherBuff : AttributeBuff
{
    // Amount of max ammo to add
    private int ammo = 4;
    // Reload multiplier (double)
    private float reloadSpeed = 0.5f;

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Simply add stats
        // Increase ammo
        stats.BonusMaxAmmo += ammo;
        // Increase reload speed
        stats.BonusReloadTime += reloadSpeed;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove given stats
        // Decrease ammo
        stats.BonusMaxAmmo -= ammo;
        // Decrease reload speed
        stats.BonusReloadTime -= reloadSpeed;
    }
}
