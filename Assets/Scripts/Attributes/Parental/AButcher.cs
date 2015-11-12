class AButcher : HeroAttribute
{
    /*
        Attributes have the following fields:
        string name        - Name of the attribute
        string description - String describing the attribute, how it describes the hero
        string effect      - Description of the actual effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */

    public AButcher(HeroStats stats) : base(stats)
    {
        name = "Butcher";
        description = "[Parent] sliced only the finest cuts of fresh meat.";
        effect = "Heals every 4 kills";
        buff = new ButcherBuff();
    }
}
