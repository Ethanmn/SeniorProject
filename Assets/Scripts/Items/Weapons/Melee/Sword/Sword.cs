﻿using UnityEngine;

class Sword : MeleeWeapon
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

    public Sword(Transform hero) : base(hero)
    {
        name = "Sword";

        // Sword swing timer: short
        swingTime = 0.3f;

        // Sword base damage: 1
        damage = 1;

        // Sword knockback
        knockback = 4f;

        // Load up the attack 
        attack = Resources.Load("Prefabs/SwordAttack") as GameObject;

        // Sword sprite
        //sprite = Resources.Load("Sprites/Sword") as Sprite;
        name = "Sword";

        attackOffset = new Vector2(0.64f, 0);
    }
}
