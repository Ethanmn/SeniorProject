using UnityEngine;
using System;
using System.Collections.Generic;

class HeroInventory : MonoBehaviour
{
    // Item equipment slots
    // Heirloom slot
    private Heirloom heirloom;
    public Heirloom Heirloom
    {
        get
        {
            return heirloom;
        }
    }

    // Active slot
    private Active active;
    public Active Active
    {
        get
        {
            return active;
        }
    }

    // Trinkets
    private Trinket trinket;
    public Trinket Trinket
    {
        get
        {
            return trinket;
        }
    }
    // Trinket from Collector attribute
    private Trinket collectorTrinket;
    public Trinket CollectorTrinket
    {
        get
        {
            return collectorTrinket;
        }
    }

    void Start()
    {
        heirloom = null;
        active = null;
        trinket = null;
        collectorTrinket = null;
    }

    void Update()
    {

    }

    // Returns true if equiped or false otherwise
    public bool Equip(Heirloom hrlm)
    {
        // IF there is no weapon equiped yet
        if (heirloom == null)
        {
            Debug.Log("Equipping heirloom!");
            // Equip the weapon
            heirloom = hrlm;
            // Run OnEquip
            heirloom.OnEquip();

            return true;
        }
        // ELSE
        else
        {
            Debug.Log("There is already an heirloom!");
        }
        //item.OnEquip();
        return false;
    }

    // Returns true if equiped or false otherwise
    public bool Equip(Trinket trnk)
    {
        // CHECK FOR EXTRA SLOT FROM COLLECTOR

        // IF there is already a trinket AND it is not a Dark Mark
            // Unequip it
            // Drop it on the ground
        // Equip the new trinket
        trinket = trnk;
        return true;
    }

    // Returns true if equiped or false otherwise
    public bool Equip(Active act)
    {
        // IF there is already an active
            // Unequip it
            // Drop it on the ground
        // Equip the new trinket
        active = act;
        return true;
    }
}
