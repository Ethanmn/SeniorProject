class AAdventurer : HeroAttribute
{
    /*
        Attributes have the following fields:
        string name        - Name of the attribute
        string description - String describing the attribute, how it describes the hero
        string effect      - Description of the actual effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */

    public AAdventurer(HeroStats stats) : base(stats)
    {
        name = "Adventurer";
        description = "Like [parent], like [child].";
        effect = " +1 HP, +1 Damage";
        buff = new AdventurerBuff();
    }
}
