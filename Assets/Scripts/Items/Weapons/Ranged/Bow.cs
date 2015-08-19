﻿// Arrow goes through things, look into raycasting for hitting (search for "DontGoThroughThings")
// Can roll while charging (lol)

using UnityEngine;

class Bow : RangedWeapon
{
    /* 
        Inherited attributes:
          Sprite itemSprite  - Sprite of the weapon in the inventory
          Sprite itemName    - String used for item in inventory

          float swingTimer   - Time between attacks / Attack speed
          int damage         - Base damage the weapon does
          float knockback      - The amound of knockback the weapon does
          GameObject attack  - Prefab GameObject generated by the weapon

          int maxAmmo        - Number of shots that can loaded at once
          int minAmmo        - Number of ammo need to even shoot
          int curAmmo        - Number of ammo that the weapon currently has
          int reloadAmmo     - Number of ammo reloaded every timer
          float reloadTime   - Amount of time it takes to reload one reloadAmmo
          float speed        - Speed of the projectile
    */

    // Timer used to count how long the bow is held
    private float chargeTimer;
    // Scale of speed gained / second
    private float speedScale = 11f;
    // Flag for slow set
    private bool slowed = false;
    // Amount of slow
    private float slowSpeed = 0.5f;
    // Base damage
    private int damageScale = 2;
    // Amount of time for splits
    private float chargeTime = 0.5f;

    public Bow(Transform hero) : base(hero)
    {

        chargeTimer = 0;

        // Bow swing timer: long (
        swingTime = 0.0f;
        // Bow base damage: low - high
        damage = 0;
        // Bow knockback
        // Lance knockback
        knockback = 5f;
        // Load up the attack object
        attack = Resources.Load("Prefabs/Bullet") as GameObject;
        // Bow sprite
        //sprite = Resources.Load("Sprites/Bow") as Sprite;

        maxAmmo = 0; // + stats.BonusMaxAmmo
        minAmmo = 0;
        curAmmo = 0; // Bow has infinite ammo

        reloadAmmo = 0;
        reloadTime = 0;

        // Arrow speed variable on charge
        speed = 0;

        itemName = "Bow";
    }

    public override void OnMouseDown(Transform hero)
    {
        if (!slowed)
        {
            slowed = true;
            stats.SpeedMultiplier -= slowSpeed;
        }

        // Start counting the time
        // IF the time is less than 3 seconds
        if (chargeTimer < 2)
        {
            chargeTimer += Time.deltaTime;
        }

        base.OnMouseDown(hero);
    }

    public override void OnMouseUp(Transform hero)
    {
        // Modify the charge timer with the swing timer multiplier (specifically for bows to make them shoot faster/slower with swing timers)
        float modChargeTime = chargeTime * stats.BonusSwingTimeMultiplier;

        // IF the bow was not charged long enough
        if (chargeTimer >= chargeTime)
        {
            // Charged for 0.5 seconds
            if (chargeTimer < modChargeTime * 2)
            {
                chargeTimer = damageScale;
            }
            // Charged for 1 second
            else if (chargeTimer < modChargeTime * 3)
            {
                chargeTimer = damageScale + 1;
            }
            // Charged for 1.5 seconds
            else if (chargeTimer >= modChargeTime * 3)
            {
                chargeTimer = damageScale + 2;
            }
            
            // Add the extra damage
            stats.BonusDamage += (int)chargeTimer;
            // Add the speed
            speed = speedScale * chargeTimer;

            base.OnMouseUp(hero);
        }
        else
        {
            // Cancel the attack
            Debug.Log("Didn't charge long enough!");
        }
        

        ResetValues();
    }

    private void ResetValues()
    {
        // Reset the values
        // Remove the extra damage
        stats.BonusDamage -= (int)chargeTimer;
        // Remove the speed
        speed = 0;
        // Reset the timer
        chargeTimer = 0;
        // Reset the slow
        slowed = false;
        stats.SpeedMultiplier += slowSpeed;
    }
}