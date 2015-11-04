using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HeartGUI : MonoBehaviour {

    // The game hero
    private GameObject hero;
    // Hero's stats
    private HeroStats heroStats;

    // List of hearts
    private List<GameObject> hearts = new List<GameObject>();
    // List of temp hearts
    private List<GameObject> tempHearts = new List<GameObject>();

    // Spacing for the images
    private float spacingX;
    private float spacingY;
    private int heartsPerRow;

    // Heart Sprites
    private Sprite fullHeart;
    private Sprite emptyHeart;
    private Sprite tempHeart;

    /*
        Notes to self:
        - Update only when health or inventory content changes
        - Set up hearts
        - Set up Active item box
        - Set up Trinket item box
    */

    // Use this for initialization
    void Start () {
        // Get the hero's stats and inventory
        hero = GameObject.FindGameObjectWithTag("Hero");
        heroStats = hero.GetComponent<HeroStats>();

        // Load the sprites
        fullHeart = (Resources.LoadAll<Sprite>("Sprites/UIHeart")[0]);
        emptyHeart = (Resources.LoadAll<Sprite>("Sprites/UIHeart")[1]);
        tempHeart = (Resources.LoadAll<Sprite>("Sprites/UIHeart")[2]);

        // Set spacing (size of image)
        spacingX = 0.4f;
        spacingY = 0.4f;
        heartsPerRow = 10;

        // Set up hearts
        AddHearts(heroStats.MaxHealth);

        // Subscribe to the OnHealthChangeEvent to handle hearts
        PublisherBox.onHealthChangePub.RaiseOnHealthChangeEvent += HandleOnHealthChangeEvent;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void AddHearts(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject heart = Instantiate(Resources.Load("Prefabs/UIHeart"), transform.position, Quaternion.identity) as GameObject;
            heart.transform.SetParent(transform, false);
            heart.GetComponent<Image>().sprite = fullHeart;

            // Dependent on the number of hearts per row
            int y = -Mathf.FloorToInt(hearts.Count / heartsPerRow);
            int x = hearts.Count + y * heartsPerRow;

            // Spacing is based on the size of the object's rect transform size
            spacingX = heart.GetComponent<RectTransform>().rect.width;
            spacingY = heart.GetComponent<RectTransform>().rect.height;

            heart.transform.localPosition = new Vector3(x * spacingX + heart.transform.position.x, y * spacingY + heart.transform.position.y, heart.transform.position.z);

            hearts.Add(heart);
        }

        // Tell temp hearts to move
        ResetPositionTempHearts();
    }

    private void AddTempHearts(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject tHeart = Instantiate(Resources.Load("Prefabs/UIHeart"), transform.position, Quaternion.identity) as GameObject;
            tHeart.transform.SetParent(transform, false);
            tHeart.GetComponent<Image>().sprite = tempHeart;

            // Dependent on the number of hearts per row
            // Temp hearts are always AFTER all normal hearts
            int y = -Mathf.FloorToInt((hearts.Count + tempHearts.Count) / heartsPerRow);
            int x = (hearts.Count + tempHearts.Count) + y * heartsPerRow;

            // Spacing is based on the size of the object's rect transform size
            spacingX = tHeart.GetComponent<RectTransform>().rect.width;
            spacingY = tHeart.GetComponent<RectTransform>().rect.height;

            // Have to divide by 64 because apparently position + 1 = position + 64
            tHeart.transform.localPosition = new Vector3(x * spacingX + tHeart.transform.position.x, y * spacingY + tHeart.transform.position.y, tHeart.transform.position.z);

            tempHearts.Add(tHeart);
        }
    }

    private void RemoveHearts(int amount)
    {
        // Itterate backwards to save links
        for (int i = amount - 1; i >= heroStats.MaxHealth; i--)
        {
            // Save the object to destroy later
            GameObject heart = hearts[i];
            // Remove the object from the list
            hearts.RemoveAt(i);
            // Destroy the object
            Destroy(heart);
        }

        ResetPositionTempHearts();
    }

    private void RemoveTempHearts(int amount)
    {
        // Itterate backwards to save links
        for (int i = amount - 1; i >= heroStats.TempHealth; i--)
        {
            // Save the object to destroy later
            GameObject tHeart = tempHearts[i];
            // Remove the object from the list
            tempHearts.RemoveAt(i);
            // Destroy the object
            Destroy(tHeart);
        }
    }

    private void EmptyTempHearts()
    {
        for (int i = tempHearts.Count - 1; i >= 0; i--)
        {
            // Save the object to destroy later
            GameObject tHeart = tempHearts[i];
            // Remove the object from the list
            tempHearts.RemoveAt(i);
            // Destroy the object
            Destroy(tHeart);
        }
    }

    // Used after normal hearts are added or removed to make sure temp hearts are always in the right position
    private void ResetPositionTempHearts()
    {
        // Remove them
        EmptyTempHearts();
        // By logic order, they are automatically readded to the list
    }

    private void HandleOnHealthChangeEvent(object sender, POnHealthChangeEventArgs e)
    {
        // IF max health is less than the number of hearts displayed
        if (heroStats.MaxHealth < hearts.Count)
        {
            // Remove hearts that have been lost
            RemoveHearts(hearts.Count);
        }
        // ELSE IF max health is greater than the number of hearts displayed
        else if (heroStats.MaxHealth > hearts.Count)
        {
            AddHearts(heroStats.MaxHealth - hearts.Count);
        }

        // Update hearts that exist
        // Anything after the health number is empty
        for (int i = heroStats.Health; i < heroStats.MaxHealth; i++)
        {
            hearts[i].GetComponent<Image>().sprite = emptyHeart;
        }
        // Anything before the health is full
        for (int i = heroStats.Health - 1; i >= 0; i--)
        {
            hearts[i].GetComponent<Image>().sprite = fullHeart;
        }

        // IF the temp health has changed
        if (tempHearts.Count < heroStats.TempHealth)
        {
            AddTempHearts(heroStats.TempHealth - tempHearts.Count);
        }
        else if (tempHearts.Count > heroStats.TempHealth)
        {
            // Remove the temp hearts that have been lost
            RemoveTempHearts(tempHearts.Count);
        }
    }
}
