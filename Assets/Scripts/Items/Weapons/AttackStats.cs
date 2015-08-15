using UnityEngine;

public class AttackStats : MonoBehaviour
{
    // Damage the attack will do
    private int damage;
    // Knockback the weapon will do
    private float knockBack;

    public int Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    public float KnockBack
    {
        get
        {
            return knockBack;
        }

        set
        {
            knockBack = value;
        }
    }
}
