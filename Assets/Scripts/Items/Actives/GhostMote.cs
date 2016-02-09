using UnityEngine;
using System;
using System.Collections.Generic;

class GhostMote : Active
{    
    public GhostMote() : base()
    {
        name = "Ghost Mote";
        sprite = Resources.Load<Sprite>("Sprites/Items/GhostMote");

        // Number of enemies to kill to fully recharge
        maxCharges = 6;
        // Start at max charges
        curCharges = 6;
        // Take all charges to use
        useCharges = 6;
    }

    protected override void ActiveEffect()
    {
        // Add buff to extend flinch time
        stats.GetComponent<BuffController>().AddBuff(new GhostMoteBuff());
        // Activate flinch
        stats.Flinching = true;
    }
}
