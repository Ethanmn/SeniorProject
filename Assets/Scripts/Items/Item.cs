/* This class describes an item that the hero can find and use
 * Items can be in a player's inventory or on the ground
 */

using UnityEngine;

public abstract class Item : MonoBehaviour
{
    // String used to display name in UI
    protected string itemName;
    public string ItemName { get { return itemName; } }
    // Sprite the item will use in the inventory
    protected Sprite itemSprite;
    // Transform of the character holding the item
    protected Transform chr;

    // Things that happen when equipped
    public abstract void OnEquip(Transform chr);
    // Things that happen when unequipped
    public abstract void OnUnequip();

    public void Start()
    {

    }

    public void Update()
    {

    }

    /// <summary>
    /// What happens when the player comes into contact with this item
    /// </summary>
    /// <param name="col"></param>
    public abstract void OnCollisionEnter2D(Collision2D col);
}
