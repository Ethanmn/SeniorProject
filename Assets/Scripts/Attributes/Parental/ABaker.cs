class ABaker : HeroAttribute
{
    /*
        Attributes have the following fields:
        string name        - Name of the attribute
        string description - String describing the attribute, how it describes the hero
        string effect      - Description of the actual effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */

    public ABaker(HeroStats stats) : base(stats)
    {
        name = "Baker";
        description = "Papa baked a mean loaf.";
        effect = "Start with a [Loaf of Bread]";
        buff = null;
    }
}
