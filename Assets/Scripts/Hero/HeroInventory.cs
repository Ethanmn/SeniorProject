using UnityEngine;
using System;
using System.Collections.Generic;

class HeroInventory : MonoBehaviour
{
    // Item equipment slots
    // Weapon slot
    private Weapon weapon;

    public Weapon Weapon
    {
        get
        {
            return weapon;
        }
    }

    // Active slot
    //private [ACTIVEITEM] active;
    // Trinket
    //private Trinket trinket;

    void Start()
    {
        weapon = null;
    }

    void Update()
    {

    }

    // Returns true if equiped or false otherwise
    public bool EquipHeirloom(Weapon wpn)
    {
        // IF there is no weapon equiped yet
        if (weapon == null)
        {
            // Run OnEquip
            weapon.OnEquip();
            // Equip the weapon
            weapon = wpn;

            return true;
        }
        // ELSE
        //item.OnEquip();
        return false;
    }
}
