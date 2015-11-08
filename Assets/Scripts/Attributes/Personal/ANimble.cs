class ANimble : HeroAttribute
{
    /*
        Attributes have the following fields:
        string name        - Name of the attribute
        string description - String describing the attribute, how it describes the hero
        string effect      - Description of the actual effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */

    public ANimble(HeroStats stats) : base(stats)
    {
        name = "Nimble";
        description = "Deft are " + stats.PronounPossesive.ToLower() + " fingers, and swift "+ stats.PronounPossesive.ToLower() + " movement.";
        effect = "Attack/Shoot twice as fast";
        buff = new NimbleBuff();
    }
}
