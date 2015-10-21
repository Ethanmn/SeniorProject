using System;
using System.Collections.Generic;

class GhostMote : Active
{    
    public GhostMote() : base()
    {
        // Number of enemies to kill to fully recharge
        maxCharges = 8;
        // Start at max charges
        curCharges = 8;
        // Take all charges to use
        useCharges = 8;
    }

    protected override void ActiveEffect()
    {
        // Add buff to extend flinch time
        stats.GetComponent<BuffController>().AddBuff(new GhostMoteBuff());
        // Activate flinch
        stats.Flinching = true;
    }
}
