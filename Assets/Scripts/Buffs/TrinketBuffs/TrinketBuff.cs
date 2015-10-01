
using System;
using UnityEngine;

public abstract class TrinketBuff : Buff
{
    // Stats of the player's character
    protected HeroStats stats;

    public override void OnBegin(Transform chr)
    {
        this.chr = chr;
        stats = chr.GetComponent<HeroStats>();
    }

    public override void OnEnd()
    {
        
    }

    public override void OnUpdate()
    {
        
    }
}
