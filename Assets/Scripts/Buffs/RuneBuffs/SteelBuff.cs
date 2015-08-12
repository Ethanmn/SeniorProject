using UnityEngine;

class SteelBuff : RuneBuff
{
    private int maxStacksScale = 2;
    private float timer = 0;

    public override void OnBegin(Transform chr)
    {
        base.OnBegin(chr);

        stats = chr.GetComponent<HeroStats>();

        timer = 0;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        timer += Time.deltaTime;
        if ((level <= 2 && (int) timer == 4) ||
            (level >= 3 && (int)timer == 4))
        {
            timer = 0;
            
            if (stats.BonusDefense < maxStacksScale * level)
            {
                stats.BonusDefense++;
            }
        }
    }
}

