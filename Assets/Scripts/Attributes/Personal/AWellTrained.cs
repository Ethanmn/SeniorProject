class AWellTrained : HeroAttribute
{
    /*
        Attributes have the following fields:
        string name        - Name of the attribute
        string description - String describing the attribute, how it describes the hero
        string effect      - Description of the actual effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */

    public AWellTrained(HeroStats stats) : base(stats)
    {
        name = "Well Trained";
        description = stats.PronounPersonal + " trained in the art of war, and learn " + stats.PronounPersonal.ToLower() + " did.";
        effect = "+1 Damage, +2 increased movespeed";
        buff = new WellTrainedBuff();
    }
}
