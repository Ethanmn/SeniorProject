﻿using UnityEngine;
using System;
using System.Collections.Generic;

public class Heirloom : Item
{
    // Maximum number of runes allowed
    int maxRunes = 5;

    // What kind of weapon is the Heirloom?
    protected Weapon weapon;
    public Weapon Weapon
    {
        get { return weapon; }
    }

    // List of the runes attached to the 
    protected List<Rune> runes;
    public List<Rune> Runes
    {
        get { return runes; }
    }

    public Heirloom(Weapon weap)
    {
        weapon = weap;
        runes = new List<Rune>();
    }

    public override void OnEquip(Transform chr)
    {
        this.hero = chr;
        // Set up for weapon and runes
        weapon.OnEquip(chr);
        foreach (Rune rune in runes)
        {
            rune.OnEquip(chr);
        }

    }

    public override void OnUnequip()
    {
        // Tear down for weapon and runes
        weapon.OnUnequip();
        foreach (Rune rune in runes)
        {
            rune.OnUnequip();
        }
    }

    /// <summary>
    /// Add a rune to the Heirloom. Returns true if added, else false.
    /// </summary>
    /// <param name="rune"></param>
    /// <returns></returns>
    public bool AddRune(Rune rune)
    {

        // FOR EACH rune in the list, check if the added rune type is already in the list
        foreach (Rune r in runes)
        {
            if (r.GetType().Equals(rune.GetType()))
            {
                r.LevelUp();
                return true;
            }
        } 

        // If it is a new rune
        // IF the number of runes is less than the max allowed
        if (runes.Count < maxRunes)
        {
            Debug.Log("New Rune!");
            rune.OnEquip(hero);
            runes.Add(rune);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Changes the heirloom's weapon without changing the runes associated with it.
    /// Mostly for testing, but can also be used with the Blacksmith Attribute.
    /// Returns true if changed and false if not.
    /// </summary>
    /// <param name="weap"></param>
    /// <returns></returns>
    public bool ChangeWeapon(Weapon weap)
    {
        // Make sure it is a different weapon type
        if (!weap.GetType().Equals(weapon.GetType()))
        {
            // "Unequip" the weapon to restore all modified stats
            weapon.OnUnequip();
            // Assign the new weapon
            weapon = weap;
            // "Equip" the new weapon
            weapon.OnEquip(hero);

            // TRUE the weapon was changed
            return true;
        }
        // FALSE the weapon was not changed
        return false;
    }

    public override void Drop(Vector3 pos)
    {
        // Instantiate the item prefab
        GameObject it = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Item")) as GameObject;
        // Set it to the item to be dropped
        it.GetComponent<ItemObjectScript>().Item = this;
        // Drop it
        // Probably need more logic to not drop things outside of the play area
        it.transform.position = pos;
    }

    public override void OnDestroy()
    {
        // Destroy each of the runes
        foreach (Rune rune in runes)
        {
            rune.OnDestroy();
        }
        // Destory the weapon
        weapon.OnDestroy();
    }
}
