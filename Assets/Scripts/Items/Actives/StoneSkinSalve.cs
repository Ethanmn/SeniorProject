using System;
using System.Collections.Generic;
using UnityEngine;

class StoneSkinSalve : Active
{
    // Health restored by the item
    private int heal = 2;
    
    public StoneSkinSalve() : base()
    {
        // Item info
        name = "Stone Skin Salve";
        sprite = Resources.Load<Sprite>("Sprites/Items/StoneSkinSalve");

        // Number of enemies to kill to fully recharge
        maxCharges = 12;
        // Start at max charges
        curCharges = 12;
        // Take all charges to use
        useCharges = 12;
    }

    protected override void ActiveEffect()
    {
        // Heal the hero
        stats.TempHealth += heal;
    }
}
