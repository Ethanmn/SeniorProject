class APerfectionist : HeroAttribute
{
    /*
        Attributes have the following fields:
        string name        - Name of the attribute
        string description - String describing the attribute, how it describes the hero
        string effect      - Description of the actual effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */

    public APerfectionist(HeroStats stats) : base(stats)
    {
        name = "Perfectionist";
        description = "He would check, and check again. If it wasn't perfect, it was worthless.";
        effect = "Deal double damage at full health";
        buff = new PerfectionistBuff();
    }
}
