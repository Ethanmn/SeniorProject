using UnityEngine;

class StubbornBuff : AttributeBuff
{

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Simply add stats
        // Set the buff flag
        stats.Undershirt = true;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove given stats
        // Remove the buff flag
        stats.Undershirt = false;
    }
}
