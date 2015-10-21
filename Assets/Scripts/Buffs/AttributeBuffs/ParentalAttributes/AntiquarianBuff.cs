using UnityEngine;

class AntiquarianBuff : AttributeBuff
{
    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Simply add stats
        // Flag the antiquarian stat
        stats.Antiquarian = true;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove given stats
        // Flag the antiquarian stat
        stats.Antiquarian = false;
    }
}
