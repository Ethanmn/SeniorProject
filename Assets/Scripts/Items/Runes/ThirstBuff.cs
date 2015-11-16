public class ThirstRune : Rune
{
    /*
        Attributes have the following fields:
        string name        - Name of the rune
        string effect      - Description of the effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */
    public ThirstRune()
    {
        name = "Thirst rune";
        effect = level < 3 ? (level * 15) + "% chance on hit to restore 1 health." :
            (level * 15) + "% chance on hit to restore health equal to damage done.";
        buff = new ThirstBuff();
        // sprite = Resources.Load("Sprites/");
    }
}
