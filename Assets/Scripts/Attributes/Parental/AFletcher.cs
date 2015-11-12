class AFletcher : HeroAttribute
{
    /*
        Attributes have the following fields:
        string name        - Name of the attribute
        string description - String describing the attribute, how it describes the hero
        string effect      - Description of the actual effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */

    public AFletcher(HeroStats stats) : base(stats)
    {
        name = "Fletcher";
        description = "Arrows, bullets, and anything else that'll fly through the air, [parent] made them.";
        effect = "+4 Max Ammo, double reload speed";
        buff = new FletcherBuff();
    }
}
