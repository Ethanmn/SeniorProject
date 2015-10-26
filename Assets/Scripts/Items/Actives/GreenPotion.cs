using System;
using System.Collections.Generic;
using UnityEngine;

class GreenPotion : Active
{
    public GreenPotion() : base()
    {
        // Item sprite
        itemSprite = Resources.Load<Sprite>("Sprites/GreenPotionPH");

        // Number of enemies to kill to fully recharge
        maxCharges = 12;
        // Start at max charges
        curCharges = 12;
        // Take all charges to use
        useCharges = 12;
    }

    protected override void ActiveEffect()
    {
        // Give the buff
        stats.GetComponent<BuffController>().AddBuff(new GreenPotionBuff());
    }
}
