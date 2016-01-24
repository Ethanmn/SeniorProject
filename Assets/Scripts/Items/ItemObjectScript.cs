using UnityEngine;
using System.Collections;

public class ItemObjectScript : MonoBehaviour {

    // The item represented by the prefab
    private Item item;
    public Item Item 
    { 
        get { return item; }
        set
        {
            if (item == null)
            {
                item = value;
                gameObject.GetComponent<SpriteRenderer>().sprite = item.Sprite;
            }
        }
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnCollisionEnter2D(Collision2D col)
    {
        // If the hero collides with an item
        if (col.gameObject.CompareTag("Hero"))
        {
            HeroInventory inv = col.gameObject.GetComponent<HeroInventory>();

            // IF consumable
                // IF applicable (ie missing health)
                    // Do it
            // ELSE
            // Equip the item
            // (will automatically drop if another item is equiped)
            print("Adding " + item.Name + " to hero inventory");
            inv.Add(item);
            // Remove it from the game field
            Destroy(gameObject);
        }
    }
}
