using UnityEngine;

class SteelBuff : RuneBuff
{
    // Max number of stacks per level
    private int maxStacksScale = 2;
    // Initialize the timer
    private float timer;

    public override void OnBegin(Transform chr)
    {
        base.OnBegin(chr);
        timer = 0;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        // Increment the timer
        timer += Time.deltaTime;
        // IF level is 1-2 every 4 seconds gain a stack
        //    level is 3 every 2 seconds gain a stack
        if ((level <= 2 && (int)timer == 4) ||
            (level >= 3 && (int)timer == 2))
        {
            timer = 0;
            
            if (stats.BonusDefense < maxStacksScale * level)
            {
                stats.BonusDefense++;
            }
        }
    }
}

