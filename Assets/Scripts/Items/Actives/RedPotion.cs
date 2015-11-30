using UnityEngine;

class RedPotion : Active
{
    // Health restored by the item
    private int heal = 3;
    
    public RedPotion() : base()
    {
        // Item sprite
        sprite = Resources.Load<Sprite>("Sprites/RedPotion");
        // Name
        name = "Red Potion";

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
        if (stats.Apothecary)
        {
            control.Heal(heal * 2);
        }
        else
        {
            control.Heal(heal);
        }
    }
}
