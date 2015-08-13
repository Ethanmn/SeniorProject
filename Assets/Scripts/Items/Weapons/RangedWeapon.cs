// STILL NEED TO IMPLEMENT RELOADING

using UnityEngine;

abstract class RangedWeapon : Weapon
{
    // Number of shots that can loaded at once
    protected int maxAmmo;
    // Number of ammo need to even shoot
    protected int minAmmo;
    // Number of ammo that the weapon currently has
    protected int curAmmo;

    // Number of ammo reloaded every timer
    protected int reloadAmmo;
    // Amount of time it takes to reload on reloadAmmo
    protected float reloadTimer;

    // Speed of the projectile
    protected float speed;

    public RangedWeapon(Transform hero) : base(hero)
    {
        
    }

    public override void OnMouseUp(Transform hero)
    {
        // Check if there is ammo to shoot
        if (curAmmo >= minAmmo)
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
        curAmmo -= minAmmo;

        if (curAmmo < 0)
        {
            Debug.Log("Something went wrong with the ammo!");
            return; // ESCAPE!!
        }

        // Get the position of the mouse and player to find the angle
        Vector2 pPos = hero.position;
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Find the angle between the two vectors, then multiply it by the speed to create a velocity
        Vector2 vel = (mPos - pPos).normalized * speed;

        // Convert the player position to a Vector3
        Vector2 pos = new Vector3(pPos.x, pPos.y, 0f);

        // Create the bullet
        GameObject projectile = Object.Instantiate(attack, pos, Quaternion.identity) as GameObject;

        // Set the bullet's velocity
        projectile.gameObject.GetComponent<Rigidbody2D>().velocity = vel;

        // Find the final damage with the player's stats
        Debug.Log("Attacking for " + stats.Damage);
        projectile.gameObject.GetComponent<BulletScript>().Damage = stats.Damage;

        // Calls the OnAttackEvent
        base.Attack(hero);
    }
}