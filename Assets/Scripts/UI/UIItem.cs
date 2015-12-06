using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour {

    // Create a list of charge pieces
    List<GameObject> meter = new List<GameObject>();

    // Use this for initialization
    void Start () {
        // Add the listener for changing items
        PublisherBox.onEquipActivePub.RaiseOnEquipActiveEvent += HandleOnEquipActiveEvent;
        // Add listener for using items
        // Add listener for gaining charges (OnKill?)

        // Set the item when starting
        SetItemImage(GameObject.FindGameObjectWithTag("Hero").transform);
        SetChargeMeter(GameObject.FindGameObjectWithTag("Hero").transform);
    }
	
	// Update is called once per frame
	void Update () {
        Active item = GameObject.FindGameObjectWithTag("Hero").GetComponent<HeroInventory>().Active;
        // Fill in the amount that is in the item's charge
        for (int i = 0; i < item.CurrentCharges; i++)
        {
            meter[i].GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Sprites/UIItemChargeSlice")[0];
        }
        for (int j = item.CurrentCharges + 1; j < item.MaxCharges; j++)
        {
            meter[j].GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Sprites/UIItemChargeSlice")[1];
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
        foreach (Transform child in meterParent)
        {
            Destroy(child);
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
            slice.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Sprites/UIItemChargeSlice")[1];

            // Add it to the list
            meter.Add(slice);
        }
        
        // Fill in the amount that is in the item's charge
        for (int i = 0; i < item.CurrentCharges; i++)
        {
            meter[i].GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Sprites/UIItemChargeSlice")[0];
        }
    }
}
