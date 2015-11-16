public class SteelRune : Rune
{
    /*
        Attributes have the following fields:
        string name        - Name of the rune
        string effect      - Description of the effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */
    public SteelRune()
    {
        name = "Steel rune";
        effect = level < 3 ? "Every 4 seconds, gain a buff that blocks 1 damage from the next attack, stacking up to "+ level * 2 +" times." :
            "Every 2 seconds, gain a buff that blocks 1 damage from the next attack, stacking up to " + level * 2 + " times.";
        buff = new SteelBuff();
        // sprite = Resources.Load("Sprites/");
    }
}
