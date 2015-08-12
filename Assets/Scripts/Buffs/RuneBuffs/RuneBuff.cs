
using System;
using UnityEngine;

abstract class RuneBuff : Buff
{
    protected int level = 1;
    protected HeroStats stats;

    public override void OnBegin(Transform chr)
    {
        this.chr = chr;
        this.stats = chr.GetComponent<HeroStats>();
    }

    public override void OnEnd()
    {
        
    }

    public override void OnUpdate()
    {
        
    }

    public virtual void levelUp()
    {
        if (level <= 3)
        {
            level++;
        }
    }
}
