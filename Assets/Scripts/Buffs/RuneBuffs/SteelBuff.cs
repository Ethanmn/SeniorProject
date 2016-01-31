using UnityEngine;

class SteelBuff : RuneBuff
{
    // Max number of stacks per level
    private int maxStacksScale = 1;
    // Number of seconds for a stack
    private int stackTimer = 4;
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
        if ((level <= 2 && (int)timer == stackTimer) ||
            (level >= 3 && (int)timer == stackTimer - 2))
        {
            timer = 0;
            
            if (stats.BonusDefense < maxStacksScale * level)
            {
                stats.BonusDefense++;
            }
        }
    }
}

