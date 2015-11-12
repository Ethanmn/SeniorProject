class ARanger : HeroAttribute
{
    /*
        Attributes have the following fields:
        string name        - Name of the attribute
        string description - String describing the attribute, how it describes the hero
        string effect      - Description of the actual effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */

    public ARanger(HeroStats stats) : base(stats)
    {
        name = "Ranger";
        description = "Quite like a mouse, cunning as fox, sharp as knife, loving as [parent] bear.";
        effect = "Increased reload speed and attack speed";
        buff = new RangerBuff();
    }
}
