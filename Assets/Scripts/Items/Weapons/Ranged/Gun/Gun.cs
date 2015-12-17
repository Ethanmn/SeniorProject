﻿using UnityEngine;

class Gun : RangedWeapon
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
          int 
          Ammo     - Number of ammo reloaded every timer
          float reloadTime   - Amount of time it takes to reload one reloadAmmo
          float speed        - Speed of the projectile
    */

    public Gun(Transform hero) : base(hero)
    {
        // Gun swing timer: short
        swingTime = 0.1f;
        // Gun base damage: low
        damage = 1;
        
        // Gun knockback
        knockback = 0f;

        // Load up the attack object
        attack = Resources.Load("Prefabs/Bullet") as GameObject;
        // Gun sprite
        //sprite = Resources.Load("Sprites/Gun") as Sprite;

        maxAmmo = 6; // + stats.BonusMaxAmmo
        minAmmo = 1;
        curAmmo = maxAmmo; // Start with full ammo

        reloadAmmo = 1;
        reloadTime = 0.25f;

        speed = 15.0f;
    }
}
