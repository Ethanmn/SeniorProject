using UnityEngine;

public abstract class Item
{
    // String used to display name in UI
    protected string itemName;
    // Sprite the item will use in the inventory
    protected Sprite itemSprite;
    // Transform of the character holding the item
    protected Transform chr;

    // Things that happen when equipped
    public abstract void OnEquip();
    // Things that happen when unequipped
    public abstract void OnUnequip();
}
