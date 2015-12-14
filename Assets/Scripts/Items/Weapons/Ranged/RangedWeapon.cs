// STILL NEED TO IMPLEMENT RELOADING

using UnityEngine;

abstract class RangedWeapon : Weapon
{
    // Number of shots that can loaded at once
    protected int maxAmmo;
    // Number of ammo need to shoot
    protected int minAmmo;
    // Number of ammo that the weapon currently has
    protected int curAmmo;

    // Number of ammo reloaded every timer
    protected int reloadAmmo;
    // Amount of time it takes to reload on reloadAmmo
    protected float reloadTime;

    // Speed of the projectile
    protected float speed;

    public RangedWeapon(Transform hero) : base(hero)
    {
        
    }

    public override void OnMouseUp(Transform hero)
    {
        // Check if there is ammo to shoot
        if (stats.Ammo >= stats.MinAmmo)
        {
            // Base checks "swing timer" to see if the weapon is able to attack
            base.OnMouseUp(hero);
        }
        else
        {
            Debug.Log("Not enough ammo");
        }
    }

    protected override void Attack(Transform hero)
    {
        // Consume the ammo used
        stats.Ammo -= stats.MinAmmo;

        if (stats.Ammo < 0)
        {
            Debug.Log("Something went wrong with the ammo!");
            return; // ESCAPE!!
        }

        // Get the position of the mouse and player to find the angle
        Vector2 pPos = hero.position;
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Create the attack
        GenerateAttack(pPos, mPos);

        // IF the double attack buff is on
        if (stats.DoubleAttack)
        {
            // Find the opposite direction for the mouse
            // Find the distance the pointer is away from the hero
            Vector2 dist = mPos - pPos;
            // Find that distance from the hero in the opposite direction
            mPos = pPos - dist;
            // Create the attack in the opposite direction
            GenerateAttack(pPos, mPos);
        }
        else if (stats.QuadAttack)
        {
            // Find the distance the pointer is away from the hero
            Vector2 dist = mPos - pPos;

            // Find that distance from the hero in the opposite direction
            mPos = pPos - dist;
            // Create the attack in the opposite direction
            GenerateAttack(pPos, mPos);

            // Find that distance in a perpendicular angles
            mPos = new Vector2(pPos.x + dist.y, pPos.y - dist.x);
            GenerateAttack(pPos, mPos);

            mPos = new Vector2(pPos.x - dist.y, pPos.y + dist.x);
            GenerateAttack(pPos, mPos);
        }

        // Calls the OnAttackEvent
        base.Attack(hero);
    }

    private void GenerateAttack(Vector2 pPos, Vector2 mPos)
    {
        // Find the angle between the two vectors, then multiply it by the speed to create a velocity
        Vector2 vel = (mPos - pPos).normalized * speed;

        // Create the rotation 
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, vel);

        // Caculate the player position + (offset * rotation)
        Vector2 pos = new Vector3(pPos.x, pPos.y, 0f) + (rot * new Vector3(0f, 0.25f));

        // Create the projectile
        GameObject projectile = Object.Instantiate(attack, pos, rot) as GameObject;

        // Set the projectile's velocity
        projectile.gameObject.GetComponent<Rigidbody2D>().velocity = vel;

        // Find the final damage with the player's stats
        Debug.Log("Attacking for " + stats.Damage);

        // Pass the damage to the projectile Attack stats
        projectile.gameObject.GetComponent<AttackStats>().Damage = stats.Damage;
        // Pass the knockback to the projectile Attack stats
        projectile.gameObject.GetComponent<AttackStats>().KnockBack = knockback;
    }

    public override void OnEquip(Transform chr)
    {
        // Sets the damage
        base.OnEquip(chr);

        // Set the ammo
        stats.MaxAmmo = maxAmmo;
        stats.Ammo = curAmmo;
        stats.MinAmmo = minAmmo;

        // Set the reload stats
        stats.ReloadTime = reloadTime;
        stats.ReloadAmmo = reloadAmmo;
    }

    public override void OnUnequip()
    {
        // Removes the damage
        base.OnUnequip();

        // Remove the ammo
        stats.MaxAmmo = 0;
        stats.Ammo = 0;
        stats.MinAmmo = 0;

        // Remove the reload stats
        stats.ReloadTime = 0;
        stats.ReloadAmmo = 0;
    }
}