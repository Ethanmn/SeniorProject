using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour {

    // Create a list of charge pieces
    List<GameObject> meter = new List<GameObject>();

    // Sprites for the charge meter
    Sprite[] barSprites;

    // Use this for initialization
    void Start () {
        // Add the listener for changing items
        PublisherBox.onEquipActivePub.RaiseOnEquipActiveEvent += HandleOnEquipActiveEvent;
        // Add listener for using items
        // Add listener for gaining charges (OnKill?)

        // Get the hero for initial set up
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");

        barSprites = Resources.LoadAll<Sprite>("Sprites/UI/UIItemChargeSlice");

        // If we were able to find a hero
        if (hero != null)
        {
            // Set the item when starting
            SetItemImage(hero.transform);
            SetChargeMeter(hero.transform);
        }
        else
        {
            Debug.LogError("UIItem cannot find the hero.");
        }
    }
	
	// Update is called once per frame
	void Update () {
        // Find the hero's item
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        // If there is no item or no hero, do nothing
        if (hero == null) return;
        Active item = hero.GetComponent<HeroInventory>().Active;
        // If there is no item or no hero, do nothing
        if (item == null) return;

        // Set the name of the item
        transform.Find("ItemName").GetComponent<Text>().text = item.Name;

        // Fill in the amount that is in the item's charge
        for (int i = 0; i < item.CurrentCharges; i++)
        {
            meter[i].GetComponent<Image>().sprite = barSprites[0];
        }
        for (int j = item.CurrentCharges; j < item.MaxCharges; j++)
        {
            meter[j].GetComponent<Image>().sprite = barSprites[1];
        }
    }

    void OnDestroy()
    {
        // Remove the listeners
        PublisherBox.onEquipActivePub.RaiseOnEquipActiveEvent -= HandleOnEquipActiveEvent;
    }

    private void HandleOnEquipActiveEvent(object sender, POnEquipActiveEventArgs e)
    {
        print("Equip!");
        SetItemImage(e.Hero);
        SetChargeMeter(e.Hero);
    }

    private void SetItemImage(Transform hero)
    {
        Image itemImage = transform.Find("ItemSprite").GetComponent<Image>();
        Active item = hero.GetComponent<HeroInventory>().Active;

        if (item != null)
        {
            // Make the background visible
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
            // Make it visible
            itemImage.color = new Color(1, 1, 1, 1);
            // Set the sprite
            itemImage.sprite = item.Sprite;
        }
        else
        {
            // Make the background visible
            GetComponent<Image>().color = new Color(1, 1, 1, 0);
            // Make it invisible
            itemImage.color = new Color(1, 1, 1, 0);
        }
    }

    private void SetChargeMeter(Transform hero)
    {
        // Find the meter parent
        Transform meterParent = transform.Find("ChargeBarParent");

        // Active
        Active item = hero.GetComponent<HeroInventory>().Active;
        // Number of charges the item holds
        int maxCharges = item.MaxCharges;
        // Height of the meter slices
        float sliceHeight = GetComponent<RectTransform>().rect.height / maxCharges;
        float sliceWidth = meterParent.GetComponent<RectTransform>().rect.width;

        // Remove any children it already has
        meter.Clear();
        foreach (Transform child in meterParent)
        {
            Destroy(child.gameObject);
        }

        // Create a bunch of empties for the amount of charge(sprite[1])
        for (int i = 0; i < maxCharges; i++)
        {
            // Instantiate the slice
            GameObject slice = Instantiate(Resources.Load("Prefabs/UIChargeBar")) as GameObject;

            // Set the parent
            slice.transform.SetParent(meterParent, false);

            // Get the Rect Transform
            RectTransform sliceRT = slice.GetComponent<RectTransform>();

            // Set the height
            sliceRT.sizeDelta = new Vector2(sliceWidth, sliceHeight);

            // Set the y position
            sliceRT.localPosition = new Vector3(0, sliceHeight * i);

            // Set the image to empty
            slice.GetComponent<Image>().sprite = barSprites[1];

            // Add it to the list
            meter.Add(slice);
        }
        
        // Fill in the amount that is in the item's charge
        for (int i = 0; i < item.CurrentCharges; i++)
        {
            meter[i].GetComponent<Image>().sprite = barSprites[0];
        }
    }
}
