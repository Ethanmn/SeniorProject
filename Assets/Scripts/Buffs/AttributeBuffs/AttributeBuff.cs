
using System;
using UnityEngine;

public abstract class AttributeBuff : Buff
{
    // Stats of the player's character
    protected HeroStats stats;
    // Hero controller
    protected HeroController control;

    public override void OnBegin(Transform chr)
    {
        this.chr = chr;
        stats = chr.GetComponent<HeroStats>();
        control = chr.GetComponent<HeroController>();
    }

    public override void OnEnd()
    {
        
    }

    public override void OnUpdate()
    {
        
    }

    public override void Refresh()
    {
        
    }
}
