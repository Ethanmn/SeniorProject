using UnityEngine;
class LoafOfBread : Active
{
    // Health restored by the item
    private int heal = 3;
    
    public LoafOfBread() : base()
    {
        // Item info
        sprite = Resources.Load<Sprite>("Sprites/Items/LoafofBread");
        name = "Loaf of Bread";

        // Number of enemies to kill to fully recharge
        maxCharges = 8;
        // Start at max charges
        curCharges = 8;
        // Take all charges to use
        useCharges = 8;
    }

    protected override void ActiveEffect()
    {
        // Heal the hero
        control.Heal(heal);
    }
}
