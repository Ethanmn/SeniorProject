class AWeaver : HeroAttribute
{
    /*
        Attributes have the following fields:
        string name        - Name of the attribute
        string description - String describing the attribute, how it describes the hero
        string effect      - Description of the actual effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */

    public AWeaver(HeroStats stats) : base(stats)
    {
        name = "Weaver";
        description = "Few in this world can activate runes, even fewer can weave them, [parent] did both.";
        effect = "Increased chance of rune fragments dropping";
        buff = new WeaverBuff();
    }
}
