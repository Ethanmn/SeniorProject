public class VorpalRune : Rune
{
    /*
        Attributes have the following fields:
        string name        - Name of the rune
        string effect      - Description of the effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */
    public VorpalRune()
    {
        name = "Vorpal rune";
        effect = level < 3 ? "The first attack after "+(10 - (level * 2))+" seconds, hits for deal double damage." :
            "The first attack after " + (10 - (level * 2)) + " seconds, hits for deal triple damage.";
        buff = new VorpalBuff();
        // sprite = Resources.Load("Sprites/");
    }
}
