class AStubborn : HeroAttribute
{
    /*
        Attributes have the following fields:
        string name        - Name of the attribute
        string description - String describing the attribute, how it describes the hero
        string effect      - Description of the actual effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */

    public AStubborn(HeroStats stats) : base(stats)
    {
        name = "Stubborn";
        description = "Will of iron, willingness of a donkey.";
        effect = "If taking damage at above 1 health, go down to one health instead";
        buff = new StubbornBuff();
    }
}
