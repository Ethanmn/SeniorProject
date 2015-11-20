class ADeal : HeroAttribute
{
    /*
        Attributes have the following fields:
        string name        - Name of the attribute
        string description - String describing the attribute, how it describes the hero
        string effect      - Description of the actual effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */

    public ADeal(HeroStats stats) : base(stats)
    {
        name = "Deal with the Devil";
        description = "Dark powers, given freely for a family's blood.";
        effect = "+5 damage, -3 max health. Guaranteed to pass on to the next generation";
        buff = new DealBuff();
    }
}
