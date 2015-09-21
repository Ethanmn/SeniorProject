class LoafOfBread : Active
{
    // Health restored by the item
    private int heal = 3;
    
    public LoafOfBread() : base()
    {
        // Number of enemies to kill to fully recharge
        maxCharges = 8;
        // Start at max charges
        curCharges = 8;
        // Take all charges to use
        useCharges = 8;
    }

    public override void OnEquip()
    {
        
    }

    public override void OnUnequip()
    {
        
    }

    protected override void ActiveEffect()
    {
        // Heal the hero
        control.Heal(heal);
    }
}
