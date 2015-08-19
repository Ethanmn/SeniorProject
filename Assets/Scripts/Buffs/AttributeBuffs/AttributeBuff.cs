
using System;
using UnityEngine;

abstract class AttributeBuff : Buff
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
}
