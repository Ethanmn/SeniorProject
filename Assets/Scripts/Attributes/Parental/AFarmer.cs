class AFarmer : HeroAttribute
{
    /*
        Attributes have the following fields:
        string name        - Name of the attribute
        string description - String describing the attribute, how it describes the hero
        string effect      - Description of the actual effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */

    public AFarmer(HeroStats stats) : base(stats)
    {
        name = "Farmer";
        description = "[Parent] was up at the crack of dawn, down with the sun, and hearty as an ox.";
        effect = "Healing effects are twice as effective";
        buff = new FarmerBuff();
    }
}
