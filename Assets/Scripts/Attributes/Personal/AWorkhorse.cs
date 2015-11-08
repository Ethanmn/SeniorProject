class AWorkHorse : HeroAttribute
{
    /*
        Attributes have the following fields:
        string name        - Name of the attribute
        string description - String describing the attribute, how it describes the hero
        string effect      - Description of the actual effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */

    public AWorkHorse(HeroStats stats) : base(stats)
    {
        name = "Workhorse";
        description = "Work, work. There was always more to do, and " + stats.PronounPersonal.ToLower() + " would do it all.";
        effect = "+2 HP";
        buff = new WorkhorseBuff();
    }
}
