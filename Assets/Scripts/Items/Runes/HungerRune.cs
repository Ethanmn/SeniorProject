public class HungerRune : Rune
{
    /*
        Attributes have the following fields:
        string name        - Name of the rune
        string effect      - Description of the effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */
    public HungerRune()
    {
        name = "Hunger rune";
        effect = level < 3 ? "Killing a mob grants 1 additional damage, up to a max of "+(level * 2)+". Taking damage loses all stacks." :
            "Killing a mob grants 1 additional damage, up to a max of "+(level * 2)+". Taking damage loses half stacks.";
        buff = new HungerBuff();
        // sprite = Resources.Load("Sprites/");
    }
}
