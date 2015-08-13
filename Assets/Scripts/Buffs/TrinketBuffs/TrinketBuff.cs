
using System;
using UnityEngine;

abstract class TrinketBuff : Buff
{
    // Stats of the player's character
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

    public override void AddStack()
    {
        
    }
}
