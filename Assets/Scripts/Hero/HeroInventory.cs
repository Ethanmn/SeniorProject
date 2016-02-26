using UnityEngine;
using System;
using System.Collections.Generic;

class HeroInventory : MonoBehaviour
{
    // Number of runes allowed to hold at once\
    private int runeInvSize;

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

    private List<Rune> runes;
    public List<Rune> Runes
    {
        get { return runes; }
    }

    void Start()
    {
        runeInvSize = 3;

        heirloom = null;
        active = null;
        trinket = null;
        collectorTrinket = null;
        runes = new List<Rune>();
    }

    void Update()
    {

    }

    /// <summary>
    /// Adds an item to the inventory.
    /// </summary>
    /// <param name="item">The item to add to the inventory.</param>
    /// <returns>Returns TRUE if equiped or FALSE otherwise</returns>
    public bool Add(Item item)
    {
        if (item.GetType().IsSubclassOf(typeof(Heirloom)) || item.GetType() == typeof(Heirloom))
        {
            Equip(item as Heirloom);
            return true;
        }
        else if (item.GetType().IsSubclassOf(typeof(Trinket)))
        {
            Equip(item as Trinket);
            return true;
        }
        else if (item.GetType().IsSubclassOf(typeof(Active)))
        {
            Equip(item as Active);
            return true;
        }
        else if (item.GetType().IsSubclassOf(typeof(Rune)))
        {
            PickUpRune(item as Rune);
            return true;
        }
        return false;
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
            heirloom.OnEquip(gameObject.transform);

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
        trinket = trnk;
        trinket.OnEquip(transform);
        return true;
    }

    // Returns true if equiped or false otherwise
    public bool Equip(Active act)
    {
        Debug.Log("Equipping " + act.Name);

        // IF there is already an active
        if (active != null)
        {
            // Unequip it
            active.OnUnequip();
            // Drop it on the ground
            Debug.Log("Dropping " + active.Name);
            active.Drop(gameObject.transform.position + new Vector3(0.57f, 1f, 0));
        }

        // Equip the new trinket
        active = act;
        active.OnEquip(transform);
        return true;
    }

    public bool Equip(Rune rune)
    {
        PickUpRune(rune);
        return true;
    }

    /// <summary>
    /// Function for adding a rune into the inventory
    /// </summary>
    /// <param name="rune"></param>
    public void PickUpRune(Rune rune)
    {
        // IF the rune inventory has space
        if (runes.Count < runeInvSize)
        {
            // Add it to the list
            runes.Add(rune);
        }
        // ELSE if there is NOT enough room
        else
        {
            runes[0].Drop(gameObject.transform.position + new Vector3(0.57f, 1f, 0));
            runes.RemoveAt(0);

            // Add the new rune into the list
            runes.Add(rune);
        }

    }

    void OnDestroy()
    {
        // Destroy all inventory pieces
        if (Heirloom != null)
        {
            Heirloom.OnDestroy();
        }
        if (Active != null)
        {
            Active.OnDestroy();
        }
        foreach (Rune rune in runes)
        {
            rune.OnDestroy();
        }
    }
}
