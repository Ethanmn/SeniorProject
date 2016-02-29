using UnityEngine;

class YellowPotion : Active
{
    public YellowPotion() : base()
    {
        // Item sprite
        sprite = Resources.Load<Sprite>("Sprites/Items/YellowPotion");
        // Name
        name = "Yellow Potion";
        effect = "Deal more damage for 10 seconds.";

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
