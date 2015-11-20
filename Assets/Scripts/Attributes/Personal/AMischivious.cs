class AMischievous : HeroAttribute
{
    /*
        Attributes have the following fields:
        string name        - Name of the attribute
        string description - String describing the attribute, how it describes the hero
        string effect      - Description of the actual effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */

    public AMischievous(HeroStats stats) : base(stats)
    {
        name = "Mischievous";
        description = "When things got sticky, this little devil knew exactly how to dissapear.";
        effect = "+1 second flinch time";
        buff = new MischiviousBuff();
    }
}
