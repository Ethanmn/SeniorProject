using UnityEngine;

abstract class MeleeWeapon : Weapon
{
    // The offset for each indiviudal weapon attack (some weapon attacks are larger than others)
    protected Vector2 attackOffset;

    public MeleeWeapon(Transform hero) : base(hero)
    {

    }

    protected override void Attack(Transform hero)
    {
        // Get the position of the mouse and player to find the angle
        Vector2 pPos = hero.position;
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Create the attack
        GenerateAttack(pPos, mPos);

        // IF Double attack buff is flagged
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

            // Find that distance in a perpendicular angle
            mPos = new Vector2(pPos.x + dist.y, pPos.y - dist.x);
            GenerateAttack(pPos, mPos);

            mPos = new Vector2(pPos.x - dist.y, pPos.y + dist.x);
            GenerateAttack(pPos, mPos);
        }

        base.Attack(hero);
    }

    private void GenerateAttack(Vector2 pPos, Vector2 mPos)
    {
        // Find the angle between the two vectors
        Vector2 dir = (mPos - pPos).normalized;
        float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Atan2 finds 180 to -180, we want 360
        if (ang < 0)
            ang += 360f;

        // Lock the angles that the weapon can attack at (increments of 45)
        //int fixedAng = Mathf.RoundToInt(ang / 45) * 45;

        // Find the final rotation of the attack
        Quaternion rot = Quaternion.Euler(0, 0, ang);

        // Find the offset of the attack
        Vector2 offSet = rot * attackOffset;

        // Find the position of the attack
        Vector3 pos = new Vector3(pPos.x + offSet.x, pPos.y + offSet.y, 0f);

        // Create the attack object
        GameObject slash = Object.Instantiate(attack, pos, rot) as GameObject;

        // Attach the attack's transform to the hero (make it follow)
        slash.transform.parent = GameObject.FindGameObjectWithTag("Hero").transform;

        Debug.Log("Attacking for " + stats.Damage);
        // Set the attack's damage
        slash.GetComponent<AttackStats>().Damage = stats.Damage;
        // Set the attack's knockback
        slash.GetComponent<AttackStats>().KnockBack = knockback;
    }

    public override void OnEquip(Transform chr)
    {
        base.OnEquip(chr);

        // Just to fix the ammo UI if somehow you are able to change weapons
        stats.MaxAmmo = 0;
        stats.Ammo = 0;
    }
}
