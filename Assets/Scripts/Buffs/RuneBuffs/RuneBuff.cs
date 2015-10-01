// Rune buffs can ONLY apply to the HERO
using UnityEngine;

public abstract class RuneBuff : Buff
{
    // Runes start at level 1
    protected int level = 1;
    // Runes cap at level 3
    private int levelCap = 3;
    // The stats of the hero that the buff is applied to
    protected HeroStats stats;
    // Hero Controller
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

    public virtual void AddStack()
    {
        // Runes cap at level 3
        if (level < levelCap)
        {
            level++;
        }
        Debug.Log("Level is now " + level);
    }
}
