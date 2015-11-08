class APatient : HeroAttribute
{
    /*
        Attributes have the following fields:
        string name        - Name of the attribute
        string description - String describing the attribute, how it describes the hero
        string effect      - Description of the actual effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */

    public APatient(HeroStats stats) : base(stats)
    {
        name = "Patient";
        description = "Good things come to those who wait.";
        effect = "Damage increases between attacks (up to +3)";
        buff = new PatientBuff();
    }
}
