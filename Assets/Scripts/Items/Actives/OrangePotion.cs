using UnityEngine;

class OrangePotion : Active
{
    public OrangePotion() : base()
    {
        // Item info
        sprite = Resources.Load<Sprite>("Sprites/OrangePotion");
        name = "Orange Potion";

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
        stats.GetComponent<BuffController>().AddBuff(new OrangePotionBuff());
    }
}
