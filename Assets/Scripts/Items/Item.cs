/* This class describes an item that the hero can find and use
 * Items can be in a player's inventory or on the ground
 */

using UnityEngine;

public abstract class Item
{
    // String used to display name in UI
    protected string name;
    public string Name { get { return name; } }

    // Sprite the item will use in the inventory
    protected Sprite sprite;
    public Sprite Sprite { get { return sprite; } }

    // String that describes the effect of the item
    protected string effect;
    public string Effect { get { return effect; } }

    // Transform of the character holding the item
    protected Transform hero;

    // Things that happen when equipped
    public abstract void OnEquip(Transform chr);
    // Things that happen when unequipped
    public abstract void OnUnequip();

    public abstract void OnDestroy();

    /// <summary>
    /// Use to drop this item on the ground at a specified area
    /// </summary>
    public abstract void Drop(Vector3 pos);
}
