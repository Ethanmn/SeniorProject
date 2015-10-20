﻿using UnityEngine;

class Hammer : MeleeWeapon
{
    /* 
        Inherited attributes:
          Sprite itemSprite    - Sprite of the weapon in the inventory
          string itemName      - Name of the item

          float swingTimer     - Time between attacks / Attack speed
          int damage           - Base damage the weapon does
          float knockback      - The amound of knockback the weapon does
          GameObject attack    - Prefab GameObject generated by the weapon
          
          Vector2 attackOffset - Vector to offset the position by (so it doesn't collide with the hero)
    */

    public Hammer(Transform hero) : base(hero)
    {
        itemName = "Hammer";

        // Hammer swing timer: Long
        swingTime = 1.0f;

        // Hammer base damage: 2
        damage = 2;

        // Hammer knockback
        knockback = 10f;

        // Load up the attack 
        attack = Resources.Load("Prefabs/HammerAttack") as GameObject;

        // Hammer sprite
        //sprite = Resources.Load("Sprites/Sword") as Sprite;
        itemName = "Hammer";

        attackOffset = new Vector2(1.28f, 0);
    }
}
