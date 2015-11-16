public class ThornRune : Rune
{
    /*
        Attributes have the following fields:
        string name        - Name of the rune
        string effect      - Description of the effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */
    public ThornRune()
    {
        name = "Thorn rune";
        effect = level < 3 ? "While standing still, attackers take "+(2 * level)+" damage." :
            "Attackers take " + (2 * level) + " damage.";
        buff = new ThornBuff();
        // sprite = Resources.Load("Sprites/");
    }
}
