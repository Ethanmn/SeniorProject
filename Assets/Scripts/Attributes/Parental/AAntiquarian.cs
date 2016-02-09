class AAntiquarian : HeroAttribute
{
    /*
        Attributes have the following fields:
        string name        - Name of the attribute
        string description - String describing the attribute, how it describes the hero
        string effect      - Description of the actual effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */

    public AAntiquarian(HeroStats stats) : base(stats)
    {
        name = "Antiquarian";
        description = "Trinkets, doodads, and whichamacallits, [parent] knew them all.";
        effect = "Active items require 2 less charges (minimum 1)";
        buff = new AntiquarianBuff();
    }
}
