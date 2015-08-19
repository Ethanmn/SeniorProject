using UnityEngine;

class TumbleBuff : RuneBuff
{
    private float rollReductionScale = 0.25f;
    private float reduction = 0;

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        chr = character;

        stats = chr.GetComponent<HeroStats>();

        // IF the level is 2 or less, reduce fatigue by the scale
        if (level <= 2)
        {
            reduction = level * rollReductionScale;
        }
        // ELSE IF level is 3 or greater, remove all fatigue
        else if (level >= 3)
        {
            reduction = stats.TiredTimerRaw;
        }

        // Add the value to the to the timer reduction
        stats.BonusTiredTimer += reduction;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove any bonus added
        stats.BonusTiredTimer -= reduction;
    }
}
