using UnityEngine;

class AntiquarianBuff : AttributeBuff
{
    // Amount of max move speed to work
    private float healthMult = 1.0f;

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
