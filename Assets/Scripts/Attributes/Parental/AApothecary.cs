class AApothecary : HeroAttribute
{
    /*
        Attributes have the following fields:
        string name        - Name of the attribute
        string description - String describing the attribute, how it describes the hero
        string effect      - Description of the actual effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */

    public AApothecary(HeroStats stats) : base(stats)
    {
        name = "Apothecary";
        description = "Try [parent]'s newest panacea/dinner chowder.";
        effect = "Potions are twice as effective";
        buff = new ApothecaryBuff();
    }
}
