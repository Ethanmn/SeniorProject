using UnityEngine;

class ApothecaryBuff : AttributeBuff
{
    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Simply add stats
        // Turn on stat
        stats.Apothecary = true;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove given stats
        // Turn off stat
        stats.Apothecary = false;
    }
}
