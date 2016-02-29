// Should probably do SOMETHING? Squeek?

using System;
using System.Collections.Generic;
using UnityEngine;

class DeadRat : Active
{
    
    public DeadRat() : base()
    {
        // Item info
        name = "Dead Rat";
        //sprite = Resources.Load();
        effect = "Ew.";

        // Number of enemies to kill to fully recharge
        maxCharges = 0;
        // Start at max charges
        curCharges = 0;
        // Take all charges to use
        useCharges = 0;
    }

    protected override void ActiveEffect()
    {
        // Should probably do something
    }
}
