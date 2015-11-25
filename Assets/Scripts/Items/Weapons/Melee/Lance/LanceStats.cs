using UnityEngine;

public class LanceStats : AttackStats
{
    // Already has: int Damage, float Knockback

    // Tip (top) damage
    public int tipDamage = 0;
    // Blunt (middle) damage
    public int bluntDamage { get { return Damage; } }
    // Base (bottom) damage
    public int baseDamage = 0;

}
