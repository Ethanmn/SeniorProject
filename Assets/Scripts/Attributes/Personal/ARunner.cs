class ARunner : HeroAttribute
{
    /*
        Attributes have the following fields:
        string name        - Name of the attribute
        string description - String describing the attribute, how it describes the hero
        string effect      - Description of the actual effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */

    public ARunner(HeroStats stats) : base(stats)
    {
        name = "Runner";
        description = stats.PronounPersonal + " ran, and when " + stats.PronounPersonal.ToLower() + " was tired, " + stats.PronounPersonal.ToLower() + " ran some more.";
        effect = "+2 increased movespeed";
        buff = new RunnerBuff();
    }
}
