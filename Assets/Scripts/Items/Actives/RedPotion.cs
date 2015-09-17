using System;
using System.Collections.Generic;

class RedPotion : Active
{
    // Health restored by the item
    private int heal = 3;
    
    public RedPotion() : base()
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
        // Heal the hero
        stats.Health += heal;
    }
}
