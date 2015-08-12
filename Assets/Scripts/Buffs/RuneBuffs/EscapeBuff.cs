using UnityEngine;

class EscapeBuff : RuneBuff
{
    private float flinchTimerScale = 0.5f;
    private float timerBonus = 0;

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Increate the flinching timer
        timerBonus += level * flinchTimerScale;

        // If the level is 3, double the value
        if (level >= 3)
        {
            timerBonus *= 2;
        }

        stats.FlinchTimerBonus += timerBonus;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        stats.FlinchTimerBonus -= timerBonus;
    }
}
