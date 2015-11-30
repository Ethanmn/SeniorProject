using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Add the listener for changing items
        PublisherBox.onEquipActivePub.RaiseOnEquipActiveEvent += HandleOnEquipActiveEvent;
        // Add listener for using items

        // Set the item when starting
        SetItemImage(GameObject.FindGameObjectWithTag("Hero").transform);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnDestroy()
    {
        // Remove the listeners
        PublisherBox.onEquipActivePub.RaiseOnEquipActiveEvent -= HandleOnEquipActiveEvent;
        // Remove listeners
    }

    private void HandleOnEquipActiveEvent(object sender, POnEquipActiveEventArgs e)
    {
        SetItemImage(e.Hero);
    }

    private void SetItemImage(Transform hero)
    {
        Image itemImage = transform.Find("ItemSprite").GetComponent<Image>();
        Active item = hero.GetComponent<HeroInventory>().Active;

        if (item != null)
        {
            // Make it visible
            itemImage.color = new Color(1, 1, 1, 1);
            // Set the sprite
            itemImage.sprite = item.Sprite;
        }
        else
        {
            // Make it invisible
            itemImage.color = new Color(1, 1, 1, 0);
        }
    }
}
