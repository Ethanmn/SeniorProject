using System;
using System.Collections.Generic;

class GreenPotion : Active
{
    public GreenPotion() : base()
    {
        // Number of enemies to kill to fully recharge
        maxCharges = 12;
        // Start at max charges
        curCharges = 12;
        // Take all charges to use
        useCharges = 12;
    }

    public override void OnEquip()
    {
        
    }

    public override void OnUnequip()
    {
        
    }

    protected override void ActiveEffect()
    {
        // Give the buff
        stats.GetComponent<BuffController>().AddBuff(new GreenPotionBuff());
    }
}
