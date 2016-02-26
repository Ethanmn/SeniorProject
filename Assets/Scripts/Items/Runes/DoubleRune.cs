using UnityEngine;

public class DoubleRune : Rune
{
    /*
        Attributes have the following fields:
        string name        - Name of the rune
        string effect      - Description of the effect (ie +1 HP)
        AttributeBuff buff - Buff associated with the attribute
    */
    public DoubleRune()
    {
        sprite = Resources.Load<Sprite>("Sprites/Items/GenericRune");

        name = "Double rune";
        effect = level < 3 ? "Every " + (5 - level) + " attacks, your attack also attacks in the opposite direction." :
            "Every 2 attacks, your attack also attacks in all directions.";
        buff = new DoubleBuff();
        // sprite = Resources.Load("Sprites/");
    }
}
