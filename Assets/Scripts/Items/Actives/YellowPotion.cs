using UnityEngine;

class YellowPotion : Active
{
    public YellowPotion() : base()
    {
        // Item sprite
        sprite = Resources.Load<Sprite>("Sprites/YellowPotion");
        // Name
        name = "Yellow Potion";

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
        stats.GetComponent<BuffController>().AddBuff(new YellowPotionBuff());
    }
}
